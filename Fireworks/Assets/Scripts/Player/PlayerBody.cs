using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    float movement_speed = 20f, max_speed = 20f;
    float move;
    bool jump, hurt;

    JumpChecker ground_checker, r_wall_checker, l_wall_checker;
    int grounded, jumps_left, max_jumps = 1;
    float jump_cooldown = 0.1f, jump_timer, jump_force = 20f, wall_force = 12f,
        move_cooldown = 0.5f, move_timer;
    float jump_multiplier = 4f;
    float fall_multiplier = 4.5f;

    Animator anim;
    int jump_hash, speed_hash, wall_hash, air_hash, hurt_hash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        foreach (JumpChecker checker in GetComponentsInChildren<JumpChecker>())
            switch (checker.type)
            {
                case JumpCheckerType.Ground:
                    ground_checker = checker;
                    break;
                case JumpCheckerType.LeftWall:
                    l_wall_checker = checker;
                    break;
                case JumpCheckerType.RightWall:
                    r_wall_checker = checker;
                    break;
                default:
                    break;
            }
        ground_checker.GroundEvent += OnGround;
        l_wall_checker.GroundEvent += OnGround;
        r_wall_checker.GroundEvent += OnGround;

        anim = GetComponent<Animator>();

        jump_hash = Animator.StringToHash("Jump");
        air_hash = Animator.StringToHash("Air");
        speed_hash = Animator.StringToHash("Speed");
        wall_hash = Animator.StringToHash("Wall");
        hurt_hash = Animator.StringToHash("Hurt");
    }

    // Update is called once per frame
    void Update()
    {
        //move = Input.GetAxis("Horizontal");
        //jump = (Input.GetButtonDown("Jump") || jump) && jumps_left > 0;
        CheckCooldown(ref jump_timer);
        CheckCooldown(ref move_timer);
    }

    public void HandleInput(float move, bool jump)
    {
        this.move = move;
        this.jump = (jump || this.jump) && jumps_left > 0;
    }

    void Jump()
    {

        if ((jumps_left > 0) || 
            ((ground_checker.grounded > 0 || l_wall_checker.grounded > 0 || r_wall_checker.grounded > 0) 
            && float.IsNaN(jump_timer)))
        {
            jump = false;
            jump_timer = jump_cooldown;
            jumps_left--;
            anim.SetTrigger(jump_hash);
            //FX: jump

            if (ground_checker.grounded > 0)
                rb.velocity = new Vector2(rb.velocity.x, jump_force);
            else if ((l_wall_checker.grounded > 0 || r_wall_checker.grounded > 0))
            {
                move_timer = move_cooldown;
                float force = (l_wall_checker.grounded > r_wall_checker.grounded) ? wall_force : -wall_force;
                rb.velocity = new Vector2( force, jump_force);
            }
            else
                rb.velocity = new Vector2(rb.velocity.x, jump_force);
                
        }
    }

    private void FixedUpdate()
    {
        if (l_wall_checker.grounded == 0 && r_wall_checker.grounded == 0)
        {
            if (rb.velocity.y < 0f)
                rb.velocity += Vector2.up * Physics.gravity.y * (fall_multiplier - 1f) * Time.deltaTime;
            if (rb.velocity.y > 0f)
                rb.velocity += Vector2.up * Physics.gravity.y * (jump_multiplier - 1f) * Time.deltaTime;
        }

        anim.SetBool(hurt_hash, hurt);
        if (!hurt)
        {
            if (float.IsNaN(jump_timer) && float.IsNaN(move_timer) && Mathf.Abs(move) > 0)
                if ((ground_checker.grounded > 0) || (move > 0 && r_wall_checker.grounded == 0) ||
                    (move < 0 && l_wall_checker.grounded == 0))
                    rb.velocity = new Vector2(move * movement_speed, rb.velocity.y);

            anim.SetBool(wall_hash, ground_checker.grounded == 0 && (l_wall_checker.grounded > 0 || r_wall_checker.grounded > 0));
            anim.SetBool(air_hash, grounded == 0);

            if (jump)
                Jump();
        }

        if (Mathf.Abs(rb.velocity.x) > max_speed)
            rb.velocity = new Vector2(max_speed * Mathf.Sign(rb.velocity.x), rb.velocity.y);

        anim.SetFloat(speed_hash, Mathf.Min(Mathf.Abs(move), Mathf.Abs(rb.velocity.x)));

        bool flipSprite = ((spriteRenderer.flipX ? (rb.velocity.x > 1f) : (rb.velocity.x < -1f)) && Mathf.Abs(move)>0);
        if (flipSprite)
            spriteRenderer.flipX = !spriteRenderer.flipX;

        /*
        if (rb.velocity.x > 0 && staff_point.localPosition.x < 0)
            staff_point.localPosition *= -Vector2.right;
        else if (rb.velocity.x < 0 && staff_point.localPosition.x > 0)
            staff_point.localPosition *= -Vector2.right;
        */
    }

    void CheckCooldown(ref float timer)
    {
        if (float.IsNaN(timer))
            return;
        else if ((timer -= Time.deltaTime) <= 0)
            timer = float.NaN;
    }

    void OnGround(bool ground)
    {
        grounded += (ground) ? 1 : -1; jumps_left = max_jumps;
        if (grounded < 0)
            grounded = 0;
    }
}
