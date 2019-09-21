using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject portalBlockPrefab;

    [SerializeField]
    private GameObject grid;

    public void Start()
    {
        // Get player's current level from GameManager then create the portal        
        // GameManager.Instance

        // 
        Debug.Log("success. creating level");

        Vector3 position = new Vector3(x: 8f, y: 17.5f);
        portalBlockPrefab.GetComponent<PortalBlock>().level = 1;
        GameObject block = GameObject.Instantiate(original: portalBlockPrefab, position: position, rotation: Quaternion.identity, parent: grid.transform);
    }

    public static void GoToLevel(int level)
    {
        Debug.LogError("STOPPED HERE");
        // GameManager.Instance.SetCurrentLevel()
        SceneChanger.Instance.FadetoScene(1); // fade to endless mode scene
    }
}
