using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKit : MonoBehaviour
{
    public static GameKit instance;

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
}
