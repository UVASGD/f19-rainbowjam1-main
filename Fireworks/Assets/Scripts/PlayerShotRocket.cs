using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotRocket : MonoBehaviour
{
    public GameObject explosionfx;
    public float speed = 5;
    Rigidbody2D rigid;

    public bool debug;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (debug)
        LaunchFirework(Vector2.up);
    }



    void LaunchFirework(Vector2 dir)
    {
        transform.up = dir;
        rigid.velocity = dir * speed;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Explode();
    }

    private void Explode()
    {
        if (explosionfx)
        {
            Instantiate(explosionfx, transform.position, Quaternion.identity);
            
          
        }

        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
