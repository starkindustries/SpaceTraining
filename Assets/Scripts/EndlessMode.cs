using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndlessMode : MonoBehaviour
{
    public GameObject[] blockPrefabs;
    public Tilemap tilemap;
    public int levelWidth;

    // This is the grid coordinate of the top left block position,
    // which all other blocks can be positioned off of via an offset
    private Vector3 topLeftBlockPosition = new Vector3(x: -0.5f, y: 23.5f);

    // These are for convenience when adding or subtracting position offsets.
    private Vector3 x1 = new Vector3(x: 1, y: 0);
    private Vector3 y1 = new Vector3(x: 0, y: 1);

    // Start is called before the first frame update
    private void Start()
    {
        // Create three rows of blocks from the top 
        GameObject block = Instantiate(original: blockPrefabs[0], position: new Vector3(.5f, 23.5f), rotation: Quaternion.identity, parent: tilemap.transform);
        // blocks[0]
        block.transform.position = block.transform.position + new Vector3(x: 1, y: 0);
    }

    // Update is called once per frame
    private void Update()
    {
        // on criteria, move old blocks down one level
        // generate new round of blocks above
    }

    private void GenerateRowOfBlocks()
    {

    }

    private void MoveBricksDown()
    {

    }
    /*
    public void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        // Do not allow editing palettes
        if (brushTarget.layer == 31)
            return;

        int index = Mathf.Clamp(Mathf.FloorToInt(GetPerlinValue(position, m_PerlinScale, k_PerlinOffset) * m_Prefabs.Length), 0, m_Prefabs.Length - 1);
        GameObject prefab = m_Prefabs[index];
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        if (instance != null)
        {
            Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint Prefabs");
            Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");
            instance.transform.SetParent(brushTarget.transform);
            instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, m_Z) + new Vector3(.5f, .5f, .5f)));
        }
    }*/
}
