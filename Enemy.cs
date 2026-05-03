using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     private static Enemy instance;
     public GameObject damageText;
     public static Enemy Instance { get { return instance; } }

    // Start is called before the first frame update
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
    
 public void Desaparecer()
 {
    gameObject.SetActive(false);
    
 }

      public void EnemyHit()
      {
        if (damageText != null)
        {
            var damage = Instantiate (damageText, transform.position, Quaternion.identity);
            damage.SendMessage("SetText");
        }
      }
}

 

