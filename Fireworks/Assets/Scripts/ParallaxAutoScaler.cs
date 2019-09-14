using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxAutoScaler : MonoBehaviour
{
    public float scale = 8;
    // Start is called before the first frame update
    void Awake()
    {
        List<PARALLAXATIVE> p = new List<PARALLAXATIVE>(GetComponentsInChildren<PARALLAXATIVE>());
        for (int i = 1; i <= p.Count; i++)
        {
            p[i].transform.localScale = new Vector3(p[i].transform.localScale.x, p[i].transform.localScale.y, i*scale);
        }
    }
}
