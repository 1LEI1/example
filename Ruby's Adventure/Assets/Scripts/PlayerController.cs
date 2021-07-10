using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///
///<summary>
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D collider;
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public static int Cherry;
    private bool isJumping;

    private void Start()
    {
        
    }
    private void Update()
    {
        Movement();
        SwitchAnim();
    }
    void Movement()
    {
        float horizontalMove;
        float facedircetion = Input.GetAxisRaw("Horizontal");
        horizontalMove =  Input.GetAxis("Horizontal");
        //角色移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("Running", Mathf.Abs(facedircetion));
        }
        if (facedircetion != 0)
        {
            this.transform.localScale = new Vector3(facedircetion, 1, 1);
        }
        //角色跳跃
        if (Input.GetButtonDown("Jump")&&isJumping == false) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            anim.SetBool("Jumping", true);
            isJumping = true;

        }
    }

    void SwitchAnim()
    {
        anim.SetBool("Idle", false);
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        else if (collider.IsTouchingLayers(ground))
            {
                anim.SetBool("Falling", false);
                anim.SetBool("Idle", true);
            }

    }
    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry++;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
