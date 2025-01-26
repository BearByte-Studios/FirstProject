using System.Collections.Generic;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using Mirror;

public class Character_Movement : NetworkBehaviour
{
    Vector3 movement;
    float horizontalMovement;
    float verticalMovement;

    float speed = 50f;

    float movement_num;

    public Rigidbody rb;

    
    
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
          Controller();
        }
    }


    public void Controller()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        movement_num = Mathf.Abs(verticalMovement) + Mathf.Abs(horizontalMovement);

        movement = new Vector3(horizontalMovement, 0, verticalMovement).normalized;
        if (movement_num > 0)
        {
           rb.AddForce(movement * speed * 10f * Time.deltaTime );
           transform.rotation = Quaternion.LookRotation(movement);
           rb.linearDamping = .2f;

        }

        

       

    }
}
