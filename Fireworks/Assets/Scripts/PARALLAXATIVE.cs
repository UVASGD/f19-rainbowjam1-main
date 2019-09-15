using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PARALLAXATIVE : MonoBehaviour {
    Vector3 origin;
    float positionMultiplier;
    Transform cam;

    Vector3 target;
    Vector3 velRef;

    void Start () {
        origin = transform.position;
        positionMultiplier = 1f - 1f / (origin.z + 1f);
        cam = Camera.main.transform;
    }

    void Update () {
        target = (Vector3)((Vector2)origin + (Vector2)cam.position * positionMultiplier) + Vector3.forward * origin.z;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velRef, 0.01f);
    }
}