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

    // Origin is the grid coordinate of the top left block position,
    // which all other blocks can be positioned off of via an offset
    private Vector3 origin = new Vector3(x: -0.5f, y: 23.5f);
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
            BrickBreaker.ShiftBlocksDown(blocks: blocks);
            List<GameObject> row = BrickBreaker.GenerateRowOfBlocks(rowLength: levelWidth, blocks: blockPrefabs, skipPercentage: 0f, origin: origin, parent: tilemap);
            blocks.AddRange(row);
        }        
    }
}
