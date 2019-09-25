using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private Animator animator;
    private int sceneToLoad;

    // Singleton pattern
    // https://gamedev.stackexchange.com/a/116010/123894
    private static SceneChanger _instance;
    public static SceneChanger Instance { get { return _instance; } }

    private void Awake()
    {
        // Singleton Enforcement Code
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadetoScene(int sceneIndex)
    {
        Debug.Log("Fading to scene " + sceneIndex);
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
        animator.SetTrigger("FadeIn");
    }
}
