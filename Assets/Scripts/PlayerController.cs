using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    public float speed;
    float movement;
    public float jumpValue;
    Animator anim;
    SpriteRenderer sr;
    bool isJump;


    
    // Start is called before the first frame update
    void Start()
    {
        //Practice practice = new Practice();
        //practice.health = 10;
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        isJump = false;
        //Debug.Log( "Health in Player Controller " +practice.health);


    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        movement = Input.GetAxis("Horizontal")*speed * Time.deltaTime*100;
        playerRb.velocity= new Vector2(movement,playerRb.velocity.y);
        if(playerRb.velocity.y<=0.01)
        anim.SetFloat("Speed", Mathf.Abs(playerRb.velocity.x));
        // Vertical Movement
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpValue);
            anim.SetTrigger("Jump");
            isJump = true;
        }
        if (Mathf.Abs(playerRb.velocity.y)< 0.01)
            isJump = false;

        // Attack
        if (Input.GetMouseButton(0))
            anim.SetTrigger("Attack");

        //Debug.Log(playerRb.velocity.y);

        //Flipping Sprite
        FlipSprite();
    }

    private void FixedUpdate()
    {
        // Horizontal Movement
        movement = Input.GetAxis("Horizontal") * speed * Time.deltaTime * 100;
        playerRb.velocity = new Vector2(movement, playerRb.velocity.y);
        if (playerRb.velocity.y <= 0.01)
            anim.SetFloat("speed", Mathf.Abs(playerRb.velocity.x));
        // Vertical Movement
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpValue);
            anim.SetTrigger("Jump");
            isJump = true;
        }
        if (Mathf.Abs(playerRb.velocity.y) < 0.01)
            isJump = false;
    }
    void FlipSprite()
    {
        if (Input.GetAxis("Horizontal") > 0)
            sr.flipX = true;
        if (Input.GetAxis("Horizontal") < 0)
            sr.flipX = false;
    }
}
