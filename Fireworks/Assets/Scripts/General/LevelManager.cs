using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }

    public void RestartLevel()
    {
        TransitionScreen.instance.FadeOutDone += Restart;
        TransitionScreen.instance.StartFade();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TransitionScreen.instance.FadeOutDone -= Restart;
    }
}
