using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerBody pb;
    [HideInInspector] public bool activated = true;
    // Start is called before the first frame update
    void Awake()
    {
        pb = gameObject.GetComponent<PlayerBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && pb.can_input)
        {
            float move = Input.GetAxis("Horizontal");
            bool jump = Input.GetButtonDown("Jump");
            pb.HandleInput(move, jump);
        }
    }
}
