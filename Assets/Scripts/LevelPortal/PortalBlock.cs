using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBlock : MonoBehaviour
{
    [SerializeField]
    private Material whiteFlash = null;
    private Material defaultMaterial;

    [SerializeField]
    private int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only count collisions from bullets
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("portal block struct: " + level);
            StartCoroutine(HitAnimation());
            LevelPortal.GoToLevel(level);
        }        
    }

    // White Flash Effect
    private IEnumerator HitAnimation()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Enable white flash material
        spriteRenderer.material = whiteFlash;
        yield return new WaitForSeconds(0.1f);
        
        // Re-enable default material
        spriteRenderer.material = defaultMaterial;
    }
}
