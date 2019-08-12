using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndlessMode : MonoBehaviour
{
    public GameObject[] blockPrefabs;
    public Tilemap tilemap;
    public int levelWidth;
    public float blockShiftInterval;

    // Origin is the grid coordinate of the top left position of the border
    // which all other blocks can be positioned off of via an offset
    // xoxxxxxxxxxxxxxxxxxxxxxxx  <=== here, o = origin
    // x                       x  <=== the border box
    // x                       x
    // 
    // Origin is set here so that blocks can be generated behind the top border
    // and then slide down into the playable space.
    private Vector3 origin = new Vector3(x: -0.5f, y: 24.5f);
    private float bottomY = -0.5f;
    private float timeSinceLastBlockShift;

    private List<GameObject> blocks;

    private int rowSpawnCount;
    public int rowsToSpawn = 1;

    // Start is called before the first frame update
    private void Start()
    {
        blocks = new List<GameObject>();
        timeSinceLastBlockShift = 0;
        rowSpawnCount = 0;
    }

    // Update is called once per frame
    private void Update()
    {
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
                Time.timeScale = 0;
                return;
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
                List<GameObject> row = BrickBreaker.GenerateRowOfBlocks(rowLength: levelWidth, blocks: blockPrefabs, skipPercentage: 0f, origin: origin, parent: tilemap);
                blocks.AddRange(row);
                rowSpawnCount++;
            }

            // Shift blocks down
            ShiftBlocksDown();
        }
    }
    
    private void ShiftBlocksDown()
    {
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y <= bottomY)
            {
                // Game Over
                Debug.Log("GameOver");
                Time.timeScale = 0;

                // Note because the blocks spawn in from left to right, with the lowest row spawning first,
                // the first block in the blocks array will always be the lowest leftmost block.
                // By returning if the block is at the bottom, the StartCoroutine function will never be
                // called because the first block checked is always the lowest leftmost block.
                // This prevents the unwanted behavior of blocks shifting down one more row (into the wall) 
                // after the game over condition is met.
                return;
            }
            StartCoroutine(ShiftBlock(block: block));
        }        
    }

    private IEnumerator ShiftBlock(GameObject block)
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
}
