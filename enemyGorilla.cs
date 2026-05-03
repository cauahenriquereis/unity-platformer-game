using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGorilla : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    public GameObject Carne;
    public GameObject damageText;
    private int life = 100;
    Animator anim;
    private Rigidbody2D rig;
    private static enemyGorilla instance;
 
     public static enemyGorilla Instance { get { return instance; } }
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
                transform.localScale = new Vector3(-1, 1, 1);
                patrolDestination = 1;
            }
        }

        if(patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                transform.localScale = new Vector3(1, 1, 1);
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
       public void DecreaseLife02()
{
    life = life - 50;
    anim.SetTrigger("hurt");

    if (life <= 0)
    {
        anim.SetTrigger("die");
        rig.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        rig.bodyType = RigidbodyType2D.Kinematic;
        enabled = false;
        Invoke("Death", 1.1f);
    }
}

private void Death()
{
    Instantiate(Carne, transform.position, Quaternion.identity);
    gameObject.SetActive(false);
}
   public void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.tag == "KaynMachado")
    {
        Debug.Log("Collision with player detected");
        
        // Verifica se o inimigo está andando para a esquerda
        if (patrolDestination == 1)
        {
            Debug.Log("Enemy is moving left");
            anim.SetTrigger("attack");
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
     
    
     
}
