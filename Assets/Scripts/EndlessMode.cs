using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndlessMode : MonoBehaviour
{
    public GameObject[] blockPrefabs;
    public Tilemap tilemap;
    public int levelWidth;

    // Origin is the grid coordinate of the top left block position,
    // which all other blocks can be positioned off of via an offset
    private Vector3 origin = new Vector3(x: -0.5f, y: 23.5f);    

    // Start is called before the first frame update
    private void Start()
    {
        // Create three rows of blocks from the top 
        tilemap = BrickBreaker.GenerateRowOfBlocks(rowLength: levelWidth, blocks: blockPrefabs, skipPercentage: 0f, origin: origin, parent: tilemap);       
    }

    // Update is called once per frame
    private void Update()
    {
        // on criteria, move old blocks down one level
        // generate new round of blocks above
    }

    private void MoveBricksDown()
    {

    }
}
