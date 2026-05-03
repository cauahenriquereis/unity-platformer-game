using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject damageText;
    public GameObject Carne;
    private static enemyAI instance;
 
     public static enemyAI Instance { get { return instance; } }
    private int life = 100;
    [Header ("Movement Patrol")]
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination; 
        Animator anim;
         private Rigidbody2D rig;

    
   
    
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
            DontDestroyOnLoad(gameObject);
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
                    transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 1;
                }
            }
            if(patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 0;
                }
            }
        }
    

    public void DecreaseLifeAI()
    {
        life = life - 25;
        anim.SetTrigger("hurt");
        if(life <= 0)
        {
           gameObject.SetActive(false);
           Instantiate(Carne, transform.position, Quaternion.identity);

        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "KaynMachado")
        {
            anim.SetTrigger("attack");
            
        }
    }
    
    public void Die()
      {
        rig.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        rig.bodyType = RigidbodyType2D.Kinematic;
        enabled = false;

        
      }
      public void EnemyHit(string value)
      {
        if (damageText != null)
        {
            var damage = Instantiate (damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText", value);
        }
      }
}