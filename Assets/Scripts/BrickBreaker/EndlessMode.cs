using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class EndlessMode : MonoBehaviour
{
    public GameObject[] blockPrefabs;
    public Tilemap tilemap;
    public int levelWidth;
    public float maxBlockShiftInterval;
    public float minBlockShiftInterval;
    public int minRowsToSpawn;
    [Range(0f, 1f)]
    public float maxSkipPercentage;
    [Range(0f, 1f)]
    public float minSkipPercentage;

    // Level Text
    public TextMeshProUGUI levelText;
    public Animator levelTextAnimator;    

    private float blockShiftInterval;
    private int rowsToSpawn;

    // Origin is the grid coordinate of the top left position of the border
    // which all other blocks can be positioned off of via an offset
    // xoxxxxxxxxxxxxxxxxxxxxxxx  <=== here, o = origin
    // x                       x  <=== the border box
    // x                       x
    // 
    // Origin is set here so that blocks can be generated behind the top border
    // and then slide down into the playable space.
    public Vector2 origin2D;
    private Vector3 origin;

    // bottomY is the lowest allowable block positin before GameOver
    public float bottomY;

    // This keeps the time since the last block down-shift
    private float timeSinceLastBlockShift;

    // All the blocks in the current level
    private List<GameObject> blocks;

    // Number of rows currently spawned
    private int rowSpawnCount;    

    // Player's current level
    private int currentLevel;

    // Bool to prevent multiple FadeToLevelPortal() coroutine calls
    private bool levelCompleted;

    // GameIsPaused: Bool set by manager when game is paused
    public bool gameIsPaused = false;

    // Start is called before the first frame update
    private void Start()
    {
        levelCompleted = false;
        origin = new Vector3(x: origin2D.x, y: origin2D.y);

        // Check if values make sense
        if (minSkipPercentage > maxSkipPercentage)
        {
            Debug.LogError("Min skip percentage is greater than max: " + minSkipPercentage + " " + maxSkipPercentage);
        }
        if (minBlockShiftInterval > maxBlockShiftInterval)
        {
            Debug.LogError("Min block shift interval greater than max: " + minBlockShiftInterval + " " + maxBlockShiftInterval);
        }



        // Load player data. Set current level
        currentLevel = GameManager.Instance.GetPlayerData().currentLevel;        
        SetGameData(currentLevel);

        // Play level text animation
        SetLevelAndAnimate("LEVEL " + currentLevel);

        // Generate a few rows to start the level off
        for (int i = 0; i < 10; i++)
        {
            float skipPercentage = Random.Range(min: minSkipPercentage, max: maxSkipPercentage);
            Vector3 rowOrigin = new Vector3(x: origin.x, y: origin.y - i);
            List<GameObject> row = EndlessMode.GenerateRowOfBlocks(rowLength: levelWidth, blocks: blockPrefabs, skipPercentage: skipPercentage, origin: rowOrigin, parent: tilemap);
            blocks.AddRange(row);
        }
        rowSpawnCount = 10;
    }

    // Update is called once per frame
    private void Update()
    {        
        if (levelCompleted || gameIsPaused)
        {
            return;
        }

        // Remove all null/destroyed blocks
        // https://stackoverflow.com/questions/3069748/how-to-remove-all-the-null-elements-inside-a-generic-list-in-one-go
        blocks.RemoveAll(item => item == null);

        // Check for win condition
        if (rowSpawnCount >= rowsToSpawn)
        {
            if (blocks.Count == 0)
            {
                // Mission Complete! 
                Debug.Log("MISSION COMPLETE!");
                levelCompleted = true;
                StartCoroutine(FadeToMenu());
            }
        }

        // on timer, move old blocks down one level
        // and generate new round of blocks above
        timeSinceLastBlockShift += Time.deltaTime;

        // Check blockShift timer
        if (timeSinceLastBlockShift > blockShiftInterval)
        {
            // If we've reached this point, it is time to shift all rows of blocks

            // Reset blockShift timer
            timeSinceLastBlockShift = 0;

            // Generate new row of blocks if the limit has not been reached        
            if (rowSpawnCount < rowsToSpawn)
            {
                float skipPercentage = Random.Range(min: minSkipPercentage, max: maxSkipPercentage);
                List<GameObject> row = EndlessMode.GenerateRowOfBlocks(rowLength: levelWidth, blocks: blockPrefabs, skipPercentage: skipPercentage, origin: origin, parent: tilemap);
                blocks.AddRange(row);
                rowSpawnCount++;
            }

            // Shift blocks down
            EndlessMode.ShiftBlocksDown(blocks, this);            
        }

        // Check for GameOver condition
        // Note because the blocks spawn in from left to right, with the lowest row spawning first,
        // the first block in the blocks array will always be the lowest leftmost block.
        if (blocks.Count > 0)
        {
            if (blocks[0].transform.position.y <= bottomY)
            {
                // Game Over
                Debug.Log("Block hit the bottom wall! GameOver!!");
                EndlessModeManager.Instance.DidTriggerGameOver();
            }
        }
    }        

    private IEnumerator FadeToMenu()
    {        
        // Mission complete. Show level clear text
        SetLevelAndAnimate("CLEAR");
        yield return new WaitForSeconds(2.0f);

        // Show Ad
        AdsManager.ShowVideoAdWhenReady();

        // Increment level and increase difficulty
        GameManager.Instance.IncrementCurrentLevel();

        // Transition back to LevelPortal
        SceneChanger.Instance.FadetoScene(0);

        Debug.Log("Fade to menu complete!");
    }
    
    private void SetGameData(int level)
    {
        // Reset tracker variables
        blocks = new List<GameObject>();
        timeSinceLastBlockShift = 0;
        rowSpawnCount = 0;

        // Increase base level variables dependent on level
        
        // Increase number of rows every two levels
        rowsToSpawn = minRowsToSpawn + (level - 1) / 2;
        
        // Decrease block shift interval by 0.1 per level
        blockShiftInterval = maxBlockShiftInterval - (level - 1) * 0.01f;
        if (blockShiftInterval < minBlockShiftInterval)
        {
            blockShiftInterval = minBlockShiftInterval;
        }

        Debug.Log("Level: " + level + ". Rows: " + rowsToSpawn + ". Interval: " + blockShiftInterval);
    }

    private void SetLevelAndAnimate(string level)
    {
        // Set the label's text to the current level
        levelText.text = level;

        // Set Slide animation trigger
        levelTextAnimator.SetTrigger("SlideIn");
    }

    // Shifts a list of blocks down
    // Checks for the GameOver condition
    // Returns true if gameOver or false if not.
    private static void ShiftBlocksDown(List<GameObject> blocks, MonoBehaviour gameObject)
    {
        foreach (GameObject block in blocks)
        {            
            gameObject.StartCoroutine(ShiftBlock(block: block));
        }
    }

    // Shifts a single block down one unit
    private static IEnumerator ShiftBlock(GameObject block)
    {
        // Check if block is null
        if (block == null)
        {
            yield break;
        }

        // Shift block down one position
        Vector3 oldPosition = block.transform.position;
        for(int i = 0; i <= 10; i++)
        {
            // Check if block is null *again* because it could be destroyed between waits/yields
            if (block == null)
            {
                yield break;
            }
            // The block shifts down in 10 steps. Each step is 1/10th of a second.
            block.transform.position = oldPosition + new Vector3(x: 0, y: -1) * i / 10;            
            yield return new WaitForSeconds(1 / 10);
        }        
    }

    // Generates a row of block gameObjects. 
    // rowLength:       the number of blocks (including blank blocks) to generate
    // blocks:          the different block prefabs to choose from
    // skipPercentage:  the chance that a block should be blank/skipped
    // origin:          the x, y position of the topLeft block to use as an anchor for the rest of the blocks
    // parent:          the tilemap to use as the parent obect
    private static List<GameObject> GenerateRowOfBlocks(int rowLength, GameObject[] blocks, float skipPercentage, Vector3 origin, Tilemap parent)
    {
        List<int> blocksIndexList = GenerateRowOfIndices(rowLength, blocks.Length, skipPercentage);
        List<GameObject> blocksList = new List<GameObject>();

        for (int i = 0; i < blocksIndexList.Count; i++)
        {
            int index = blocksIndexList[i];
            if (index < 0)
            {
                // skip block if index is -1
            }
            else
            {
                Vector3 position = origin + new Vector3(x: 1, y: 0) * i;
                GameObject block = GameObject.Instantiate(original: blocks[index], position: position, rotation: Quaternion.identity, parent: parent.transform);
                blocksList.Add(item: block);
            }
        }

        return blocksList;
    }

    // Returns an array of integer indices. If the index is -1 then the the block is skipped.
    // Skip percentage is the chance that a block should be blank/skipped.
    // Skip percentage should be between 0 (0%, never skip) and 1 (100%, always skip)
    private static List<int> GenerateRowOfIndices(int rowLength, int blockCount, float skipPercentage)
    {
        List<int> indices = new List<int>();

        if (skipPercentage < 0 || skipPercentage > 1)
        {
            Debug.LogError("Incorrect skipPercentage value: " + skipPercentage + ". Value should be between 0 and 1 inclusive.");
            skipPercentage = 0;
        }

        for (int i = 0; i < rowLength; i++)
        {
            // Check if this block should be blank/skipped
            float shouldSkip = Random.Range(0f, 1f);
            if (shouldSkip < skipPercentage)
            {
                // Skip block
                indices.Add(item: -1);
            }
            else
            {
                // Add a random block
                int index = Random.Range(0, blockCount);
                indices.Add(item: index);
            }
        }
        return indices;
    }
}
