using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseGenerator : MonoBehaviour
{
    public int segment_count = 5;
    float segment_w, segment_h;
    GameObject last_segment;
    List<GameObject> segments = new List<GameObject>();
    LineRenderer lr;
    Transform fuse_point;

    // Start is called before the first frame update
    void Awake()
    {
        last_segment = transform.Find("FuseSegment").gameObject;
        fuse_point = transform.Find("FusePoint");
        fuse_point.parent = transform.parent.gameObject.GetMainRigidbody2D().transform;
        segment_w = last_segment.GetComponent<CapsuleCollider2D>().size.x;
        segment_h = last_segment.GetComponent<CapsuleCollider2D>().size.y;
        segments.Add(last_segment);
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        lr.startWidth = lr.endWidth = segment_w;
        GenerateSegments();
    }

    private void Update()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            lr.SetPosition(i, segments[i].transform.position);
        }

        lr.SetPosition(0, fuse_point.position);
    }

    void GenerateSegments()
    {
        last_segment.GetComponent<HingeJoint2D>().connectedBody = transform.parent.gameObject.GetMainRigidbody2D();
        for (int i = 1; i < segment_count; i++)
        {
            GameObject next_segment = Instantiate(last_segment, 
                new Vector2(last_segment.transform.position.x, 
                last_segment.transform.position.y - segment_h),
                Quaternion.identity);
            next_segment.transform.parent = transform;
            segments.Add(next_segment);
            next_segment.GetComponent<HingeJoint2D>().connectedBody = last_segment.GetComponent<Rigidbody2D>();
            lr.positionCount++;
            lr.SetPosition(i, last_segment.transform.position);
            last_segment = next_segment;
        }
    }
}

