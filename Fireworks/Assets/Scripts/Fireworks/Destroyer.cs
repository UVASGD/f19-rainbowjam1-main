using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float death_time;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, death_time);   
    }
}
