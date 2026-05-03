using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kayn01 : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public float JumpForce;
    public bool Player2 { get; private set; } // Change to private set

     
    
    
   
    private Rigidbody2D rig;
    private BoxCollider2D bc2d;
    [SerializeField] private LayerMask layerGround;

    Animator anim;


    
    


    // Start is called before the first frame update
    void Start()
    {
        
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
        KaynMachado.Instance.DesaparecerKayn();
           
    }
        
    

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        

       
   
    }
    
    void Move()
    {
         
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        //right
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("Run", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        //left
        else if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Run", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        //stop
        else if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("Run", false);
            
        }

    }
    


    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("Jumping", true);   
        }
    }
    
    //Colisão
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            //isGrounded();
            anim.SetBool("Jumping", false);
        }
        else if (col.gameObject.tag == "Machado")
        {
            
            KaynMachado.Instance.AparecerKayn();
            col.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Player2 = true;
        }
    }

        
    

    
    private bool isGrounded()
    {
        return Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, .1f, layerGround);
        
    }
   
     
    
    void OnCollisionExit2D(Collision2D collision)
    {
         if(collision.gameObject.layer == 8)
        {
            bool isNotGrounded = !isGrounded();
        }
    }
   
   private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 7)
        {
            col.gameObject.SetActive(false);
        
        }
    }
 
    
}
