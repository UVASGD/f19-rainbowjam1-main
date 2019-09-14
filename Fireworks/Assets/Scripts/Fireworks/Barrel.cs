using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Barrel : MonoBehaviour
{
    public int firework_limit = 5;
    int firework_count;
    public float shoot_cooldown = 2f;
    float shoot_timer;
    public GameObject firework;

    Transform shoot_point;

    public float firework_speed = -1;


    Animator anim;
    int shoot_hash;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        shoot_hash = Animator.StringToHash("Shoot");
        shoot_point = transform.Find("ShootPoint");
    }

    // Update is called once per frame
    void Update()
    {
        CheckCooldown(ref shoot_timer);
        if (float.IsNaN(shoot_timer))
            Shoot();
    }

    void Shoot()
    {
        if (firework && firework_count <= firework_limit)
        {
            shoot_timer = shoot_cooldown;
            BasicFirework f = Instantiate(firework, 
                shoot_point.position, Quaternion.identity).
                GetAnyComponent<BasicFirework>(in_parent: false);
            f.transform.up = transform.up;
            firework_count++;
            if (firework_speed != -1) f.speed = firework_speed; 
            f.ExplodeEvent += delegate { firework_count--; Shoot(); };
        }
    }

    void CheckCooldown(ref float timer)
    {
        if (float.IsNaN(timer))
            return;
        else if ((timer -= Time.deltaTime) <= 0)
            timer = float.NaN;
    }
}
