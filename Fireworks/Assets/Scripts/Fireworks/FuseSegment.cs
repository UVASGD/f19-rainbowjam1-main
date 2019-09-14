using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseSegment : MonoBehaviour
{
    public GameEvent FuseSegmentCollideEvent, FuseSegmentFinishEvent;
    public float burnout_time = 2f;
    CapsuleCollider2D coll;


    // Start is called before the first frame update
    void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            FuseSegmentCollideEvent?.Invoke();
    }

    public void Light(Transform light)
    {
        StartCoroutine(Burnout(light));
    }

    IEnumerator Burnout(Transform light)
    {
        float timer = burnout_time;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            light.transform.position = Vector2.Lerp(coll.bounds.min, coll.bounds.max, 1- (timer/burnout_time));
            yield return null;
        }
        FuseSegmentFinishEvent?.Invoke();
        Destroy(gameObject);
    }
}
