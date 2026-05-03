using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minX, maxX, minY, maxY;
    public float timeLerp;
    private Kayn01 kaynScript; // Reference to Kayn01 script

    private Transform currentTarget;

    private void Start()
    {
        currentTarget = player1;
        kaynScript = player1.GetComponent<Kayn01>();
    }

   private void Update()
    {
        if (kaynScript != null && kaynScript.Player2)
        {
            currentTarget = player2;
            
        }
        else
        {
           currentTarget = player1;
        }
    }

    

    private void UpdateCameraPosition()
{
    if (currentTarget != null)
    {
        Vector3 newPosition = currentTarget.position + new Vector3(0, 0, -10);
        newPosition.z = -10;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);
        transform.position = newPosition;
    }
}


    private void FixedUpdate()
    {
        UpdateCameraPosition();
    }
}