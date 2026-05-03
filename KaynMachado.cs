using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KaynMachado : MonoBehaviour
{
    private float healingAmount = 100f;
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rig;
    bool isJumping = false;
    Animator anim;
    private Vector2 initPosition;
    
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
    initPosition = new Vector2(126f, -4f);
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
        enemyAI.Instance.DecreaseLifeAI(); 
        enemyGorilla.Instance.DecreaseLife02();
        LagartoAI.Instance.DecreaseLife03();
       

        
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
    if (col.gameObject != null)
    {
        if (col.gameObject.layer == 11)
        {
            Fase2();
            enemyAI.Instance.Die();
            enemyGorilla.Instance.Die();
            LagartoAI.Instance.Die();
        }

        if (col.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if (col.gameObject.tag == "Enemy")
        {
            Invoke("DecreaseLife", 0.8f);

            if (heartSystem.healthAmount <= 20)
            {
                Invoke("Die", 1f);
                enemyAI.Instance.Die();
                enemyGorilla.Instance.Die();
                LagartoAI.Instance.Die();
            }
        }

        if (col.gameObject.layer == 6)
        {
            SceneManager.LoadScene("GameOver");
            Destroy(rig);
            if (enemyAI.Instance != null)
            {
                enemyAI.Instance.Die();
            }

            if (enemyGorilla.Instance != null)
            {
                enemyGorilla.Instance.Die();
            }

            if (LagartoAI.Instance != null)
            {
                LagartoAI.Instance.Die();
            }
        }
        else if (col.gameObject.layer == 10)
        {
            anim.SetTrigger("eats");
            col.gameObject.SetActive(false);
            Invoke("IncreaseLife", 1.2f);
        }
    }
}

    

     private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

        
 public void DesaparecerKayn()
{
    gameObject.SetActive(false);
}
public void Respawn()
{
    // Reiniciar o estado do jogador
    anim.Rebind();
    transform.position = initPosition; // Usar a posição inicial ao ressuscitar
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

    private void Fase2()
    {
        SceneManager.LoadScene("Fase2");
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
        transform.position = new Vector2(126f, -4f);
        Invoke("LoadScene", 2f);
      }
     
    

}


