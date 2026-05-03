using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KaynFase02 : MonoBehaviour
{

    private float healingAmount = 100f;
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rig;
    bool isJumping = false;
    Animator anim;
    
    private static KaynMachado instance;
    public static KaynMachado Instance { get { return instance; } }

    [Header ("Attack Variables")]
    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask layerEnemy;
    float timeNextAttack;
    public HeartSystem heartSystem;
    public AudioSource audioS;
    public AudioClip[] Sounds;
   
    

 void Start()
    {
        
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
       if (SceneManager.GetActiveScene().name == "GameOver")
        {
            enemyAI[] enemies = FindObjectsOfType<enemyAI>();

            foreach (enemyAI enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
    
    void Update()
    {
        Move();
        Jump();
        Attack();
    }
     void Move()
    {
         
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        //right
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            
           
           
        }
        //left
        else if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
           
           
        }
        //stop
        else if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("run", false);
            
        }
        attackCheck.localPosition =  new Vector2(-attackCheck.localPosition.x, attackCheck.localPosition.y);
    }
       private void Attack()
    {
         if (timeNextAttack <= 0)
         {
            if (Input.GetButtonDown ("Fire1"))
            {
                anim.SetTrigger ("attack");
                timeNextAttack = 1.2f;
             
            }
        }
            else
            {
                timeNextAttack -= Time.deltaTime;
            }
            
         
    }
     void PlayerAttack()
{
    Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll(attackCheck.position, radiusAttack, layerEnemy);
    for (int i = 0; i < enemiesAttack.Length; i++)
    {
        enemiesAttack [i].SendMessage("EnemyHit", "-25");
        Debug.Log(enemiesAttack[i].name);
        //enemyCollider [i].SendMessage ("EnemyHit", "-5");
         EnemyBear02.Instance.DecreaseLifeAI(); 
       EnemyGorillaFase02.Instance.DecreaseLife02();
        Enemylagarto02 .Instance.DecreaseLife03();
       

        
    }
}
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }

       void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                
                anim.SetBool("jump", true);
                audioS.clip = Sounds[0];
                audioS.Play();
            }
           
            
        }
    }
      void OnCollisionEnter2D(Collision2D col)
    {

         if(col.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
         if(col.gameObject.tag == "Enemy")
        {
            Invoke("DecreaseLife", 0.8f);

            if(heartSystem.healthAmount <= 20)
            {
                Invoke("Die",1f);    
                 EnemyBear02.Instance.Die(); 
               EnemyGorillaFase02.Instance.Die();  
                Enemylagarto02 .Instance.Die();                
            }
        }

        else if(col.gameObject.layer == 10)
        {
            anim.SetTrigger("eats");
            col.gameObject.SetActive(false);
            Invoke("IncreaseLife", 1.2f);
        }

    }
    

    /* private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }*/

        
 public void DesaparecerKayn()
{
    gameObject.SetActive(false);
}
public void Respawn()
    {
        // Reiniciar o estado do jogador
        anim.Rebind();
       
        GetComponent<BoxCollider2D>().enabled = true;
        rig.bodyType = RigidbodyType2D.Dynamic;
        enabled = true;
        anim.SetBool("jump", false);
    }

public void AparecerKayn()
{
    gameObject.SetActive(true);
}
 void OnCollisionExit2D(Collision2D collision)
    {
         if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void DecreaseLife()
    {
        heartSystem.DecreaseLife();
        
    }
    private void IncreaseLife()
    {
        heartSystem.Heal(healingAmount);
    }
      private void Die()
      {
        anim.SetTrigger("death");
        rig.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        rig.bodyType = RigidbodyType2D.Kinematic;
        enabled = false;
        anim.SetBool("jump", false);
        Invoke("LoadScene", 2f);
      }
     
    

}




