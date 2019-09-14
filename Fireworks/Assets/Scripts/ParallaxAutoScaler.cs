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
        for (int i = 0; i < p.Count; i++)
        {
            p[i].transform.localPosition = new Vector3(p[i].transform.localPosition.x, p[i].transform.localPosition.y, (i+1)*scale);
        }


    }
}
