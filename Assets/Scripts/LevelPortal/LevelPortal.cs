using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject grid = null;

    [SerializeField]
    private GameObject portalBlockPrefab;
    
    [SerializeField]
    private Vector3 firstBlockPosition; // Vector3(x: 8f, y: 17.5f)

    // FF2020 red = 255, 32, 32
    private Color red = new Color(r: 255f/255, g: 32f/255, b: 32f/255);
    
    // F6621F orange = 246, 98, 31
    private Color orange = new Color(r: 246f/255, g: 98f/255, b: 31f/255);
    
    // FFEE4C yellow = 255, 238, 76
    private Color yellow = new Color(r: 255f/255, g: 238f/255, b: 76f/255);
    
    // 6DC225 green = 109, 194, 37
    private Color green = new Color(r: 109f/255, g: 194f/255, b: 37f/255);    
    
    // 4093D8 blue = 64, 147, 216
    private Color blue = new Color(r: 64f/255, g: 147f/255, b: 216f/255);
    
    // A363D9 purple = 163, 99, 217
    private Color purple = new Color(r: 163f/255, g: 99f/255, b: 217f/255);        

    public void Start()
    {        
        // Get player's current level
        int currentLevel = GameManager.Instance.GetPlayerData().currentLevel;

        Debug.Log("success. current level: " + currentLevel);

        currentLevel = 150;

        Color[] blockColors = { red, orange, yellow, green, blue, purple };

        for (int i = 0; i < currentLevel; i++)
        {
            float xPosition = firstBlockPosition.x + 2 * (i % 5);
            float yPosition = firstBlockPosition.y - 2 * (i / 5) - 3 * (i / 25);
            Debug.Log("Coord: [" + xPosition + ", " + yPosition + "]");
            GameObject block = GameObject.Instantiate(original: portalBlockPrefab, position: new Vector3(xPosition, yPosition), rotation: Quaternion.identity, parent: grid.transform);
            block.GetComponent<PortalBlock>().level = i + 1;
            block.GetComponent<SpriteRenderer>().color = blockColors[i / 25];
        }                        
    }

    public static void GoToLevel(int level)
    {
        Debug.LogError("STOPPED HERE");
        // GameManager.Instance.SetCurrentLevel()
        SceneChanger.Instance.FadetoScene(1); // fade to endless mode scene
    }
}
