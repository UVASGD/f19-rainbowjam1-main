using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint2D))]
public class FuseGrabber : MonoBehaviour
{
    List<Rigidbody2D> targeted_segments = new List<Rigidbody2D>();
    Rigidbody2D grabbed_segment;
    PlayerBody pb;
    FixedJoint2D grab_joint;
    // Start is called before the first frame update
    void Start()
    {
        pb = GetComponent<PlayerBody>();
        grab_joint = GetComponent<FixedJoint2D>();
        Detach();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && pb.can_input)
        {
            if (grabbed_segment)
                Detach();
            else if (targeted_segments.Count > 0)
            {
                grab_joint.enabled = true;
                grabbed_segment = targeted_segments[0];
                grab_joint.connectedBody = grabbed_segment;
                grabbed_segment.GetComponent<FuseSegment>().FuseSegmentFinishEvent += Detach;
            }
        }
        else if (Input.GetButtonDown("Jump") && pb.can_input)
        {
            if (grabbed_segment)
                Detach();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FuseSegment>())
            if (!targeted_segments.Contains(collision.GetComponent<Rigidbody2D>()))
                targeted_segments.Add(collision.GetComponent<Rigidbody2D>());
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<FuseSegment>())
            targeted_segments.Remove(collision.GetComponent<Rigidbody2D>());
    }

    public void Detach()
    {
        grab_joint.enabled = false;
        grab_joint.connectedBody = null;
        grabbed_segment = null;
    }
}
