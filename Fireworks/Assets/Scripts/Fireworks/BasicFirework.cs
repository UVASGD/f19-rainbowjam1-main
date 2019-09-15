using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class BasicFirework : MonoBehaviour
{
    Transform end, explosion_point;
    Vector2 dir;
    public float speed = 5;
    float explode_factor = 2f;
    Rigidbody2D rb;
    Animator anim;
    int explode_hash;
    Fuse fuse;
    public GameEvent ExplodeEvent;
    Vector2 velocity;

    public GameObject explosion_fx;

    // Start is called before the first frame update
    void Start()
    {
        explosion_point = transform.Find("explosion_point");
        anim = GetComponent<Animator>();
        explode_hash = Animator.StringToHash("Explode");
        fuse = transform.parent.GetComponentInChildren<Fuse>();
        if (fuse)
            fuse.FuseFinishEvent += Explode;

        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.velocity = transform.up * speed;
    }

    public void Explode()
    {
        ExplodeEvent?.Invoke();
        if (explosion_fx)
            Instantiate(explosion_fx, explosion_point.position, Quaternion.identity);
        rb.velocity = rb.velocity / explode_factor;
        if (anim)
        {
            anim.SetTrigger(explode_hash);
            Destroy(transform.parent.gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
        else
            Destroy(transform.parent.gameObject);
    }

}
