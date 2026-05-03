using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LagartoAI : MonoBehaviour
{
   
    //Vector3 spawnPosition = new Vector3(55f, -1f, 0f);
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    public GameObject Carne;
    public GameObject damageText;
    private int life = 100;
    Animator anim;
    private Rigidbody2D rig;
    private static LagartoAI  instance;
 
     public static LagartoAI  Instance { get { return instance; } }
    void Start()
      {
       
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
      }
      private void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(gameObject);
    }
}
            
    
    void Update()
    {
        if(patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
            {
                transform.localScale = new Vector3(-5, 5, 5);
                patrolDestination = 1;
            }
        }

        if(patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                transform.localScale = new Vector3(5, 5, 5);
                patrolDestination = 0;
            }
        }
    }
    public void EnemyHit(string value)
      {
        if (damageText != null)
        {
            var damage = Instantiate (damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText", value);
        }
      }
       public void DecreaseLife03()
    {
        life = life - 100;
        
        if(life <= 0)
        {
           anim.SetTrigger("Death");
           rig.velocity = Vector2.zero;
           GetComponent<BoxCollider2D>().enabled = false;
           rig.bodyType = RigidbodyType2D.Kinematic;
           enabled = false;
           Instantiate(Carne, transform.position, Quaternion.identity);
           gameObject.SetActive(false);
           Invoke("Death", 1.1f);

        }

    }
   public void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.tag == "KaynMachado")
    {
        Debug.Log("Collision with player detected");
        
        // Verifica se o inimigo está andando para a esquerda
        if (patrolDestination == 0)
        {
            Debug.Log("Enemy is moving left");
            anim.SetTrigger("Attack");
        }
    }
}
    public void Die()
      {
        rig.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        rig.bodyType = RigidbodyType2D.Kinematic;
        enabled = false;

        
      }
     
      private void Death()
      {
        Instantiate(Carne, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
      }
     
}

