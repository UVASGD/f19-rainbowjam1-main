using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteR;
    private new AudioSource audio;
    public List<Sprite> sprites = new List<Sprite>();
    public List<AudioClip> sounds = new List<AudioClip>();
    public float thrust = 100;
    public int fuel = 100;
    public bool exploding = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            rb.AddForce(transform.up * thrust * Time.deltaTime);
            fuel--;
            print(fuel);
            if (fuel <= 0 && !exploding)
            {
                exploding = true;
                print("BAZINGAAAAAA!!!!");
                StartCoroutine(Explode());
            }
        }
    }

    private IEnumerator Explode()
    {
        audio.clip = sounds[0];
        audio.Play();
        spriteR.sprite = sprites[1];
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);

    }
}
