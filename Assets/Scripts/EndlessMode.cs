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
    private float timeSinceLastBlockShift;

    private List<GameObject> blocks;

    // Start is called before the first frame update
    private void Start()
    {
        blocks = new List<GameObject>();
        timeSinceLastBlockShift = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        // on criteria, move old blocks down one level
        // and generate new round of blocks above
        timeSinceLastBlockShift += Time.deltaTime;
        if (timeSinceLastBlockShift > blockShiftInterval)
        {
            timeSinceLastBlockShift = 0;

            // Remove all null/destroyed blocks
            // https://stackoverflow.com/questions/3069748/how-to-remove-all-the-null-elements-inside-a-generic-list-in-one-go
            blocks.RemoveAll(item => item == null);

            // Generate new row of blocks
            List<GameObject> row = BrickBreaker.GenerateRowOfBlocks(rowLength: levelWidth, blocks: blockPrefabs, skipPercentage: 0f, origin: origin, parent: tilemap);
            blocks.AddRange(row);

            // Shift blocks down
            ShiftBlocksDown();            
        }
    }
    
    private void ShiftBlocksDown()
    {
        foreach (GameObject block in blocks)
        {
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

        Vector3 oldPosition = block.transform.position;
        for(int i = 0; i <= 10; i++)
        {            
            block.transform.position = oldPosition + new Vector3(x: 0, y: -1) * i / 10;            
            yield return new WaitForSeconds(1 / 10);
        }        
    }
}
