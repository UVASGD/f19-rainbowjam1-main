using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GameEvent();

public class Fuse : MonoBehaviour
{

    public Transform fuse_fx;
    public GameEvent FuseFinishEvent;

    #region FUSE SETUP
    GameObject last_segment;
    List<FuseSegment> segments = new List<FuseSegment>();
    List<Vector2> velocities = new List<Vector2>();
    LineRenderer lr;
    Transform fuse_point;
    public void Awake()
    {
        fuse_point = transform.Find("FusePoint");
        fuse_fx = transform.Find("fuse_fx");
        segments = new List<FuseSegment>(GetComponentsInChildren<FuseSegment>());
        fuse_point.parent = transform.parent.gameObject.GetMainRigidbody2D().transform;
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        lr.enabled = false;
    }
    void UpdateFuse()
    {
        float lerp_amnt = lr.enabled ? 0.5f: 1f;
        lr.enabled = true;

        if (segments.Count > 0)
        {
            for (int i = 0; i < segments.Count; i++)
            {
                lr.SetPosition(i, Vector2.Lerp(lr.GetPosition(i), segments[i].transform.position, lerp_amnt));
            }
            lr.SetPosition(0, fuse_point.position);
            if (segments.Count == 1)
                lr.SetPosition(1, Vector2.Lerp(lr.GetPosition(1), segments[0].transform.position, lerp_amnt ));
            else
                lr.SetPosition(segments.Count - 1, Vector2.Lerp(lr.GetPosition(segments.Count - 1), fuse_fx.position, lerp_amnt));
        }
    }
    #endregion

    private void Start()
    {   
        LightFuse();
    }

    private void Update()
    {
        UpdateFuse();
    }

    public void LightFuse()
    {
        if (fuse_fx)
            fuse_fx.GetComponent<ParticleSystem>().Play();
        if (segments.Count > 0) {
            FuseSegment fs = segments[segments.Count - 1];
            fs.Light(fuse_fx);
            fs.FuseSegmentFinishEvent += delegate { if (segments.Count > 2) lr.positionCount--; segments.Remove(fs); LightFuse(); };
        }
        else
        {
            FuseFinishEvent?.Invoke();
        }
    }
}
