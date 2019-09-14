using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PARALLAXATIVE : MonoBehaviour {
    Vector3 origin;
    float positionMultiplier;
    Transform cam;
    
    float width;
    Vector3 widthVector;
    Dictionary<Vector3, Transform> tiling;
    Vector3 leftPos;
    Vector3 midPos;
    Vector3 rightPos;

    void Start () {
        origin = transform.position;
        positionMultiplier = 1f - 1f / (origin.z + 1f);
        cam = Camera.main.transform;
        width = GetComponent<SpriteRenderer>().sprite.texture.width * GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        widthVector = Vector3.right * width;

        tiling = new Dictionary<Vector3, Transform>();
        // midPos = new Vector3(Mathf.Round())
        // tiling.Add()
    }

    void Update () {
        transform.position = (Vector3)((Vector2)origin + (Vector2)cam.position * positionMultiplier) + Vector3.forward * origin.z;

        
    }
}