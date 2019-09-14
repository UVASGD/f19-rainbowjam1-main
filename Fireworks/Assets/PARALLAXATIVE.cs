using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PARALLAXATIVE : MonoBehaviour {
    Vector3 origin;
    float positionMultiplier;
    Transform cam;

    void Start () {
        origin = transform.position;
        positionMultiplier = (origin.z + 1f);
        cam = Camera.main.transform;
    }

    void Update () {
        transform.position = (Vector3)((Vector2)origin + (Vector2)cam.position * positionMultiplier) + Vector3.forward * origin.z;
    }
}