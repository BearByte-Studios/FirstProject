using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Mirror.BouncyCastle.Asn1.Esf;
using System;

public class PickUpController : MonoBehaviour 
{
    
   
    bool isEquipped = false;

    public Transform HoldPos;

    Vector3 throwForce = new Vector3(0, 0, 100);


    
    void Update()
    {
        
        

    }


     // Checks when the player has a pickable object in front of 
     
     void PickAndDrop()
     {

        
     }
    void OnTriggerStay(Collider other)
    {
        //Uses Input to hold said object
        if(Input.GetKeyDown(KeyCode.E) && !isEquipped)
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<Collider>().enabled = false;
            other.GetComponent<Collider>().providesContacts = false;
            other.transform.localPosition = HoldPos.position;
            other.transform.parent = HoldPos.parent;
            isEquipped = true;

        }


         //Potential Throw Implementation
        if(Input.GetKeyDown(KeyCode.F) && isEquipped)
        {
            Debug.Log("Throwing");
            other.transform.parent = null;
            other.GetComponent<Rigidbody>().isKinematic = false;
            //other.GetComponent<Collider>().enabled = true;
            other.GetComponent<Collider>().providesContacts = true;
            other.GetComponent<Rigidbody>().AddForce(throwForce);
            isEquipped = false;

        }
        
        
            
        
       
    }

    


    void PickUp()
    {
        isEquipped = true;
        
    }

    void Drop()
    {



    }









    
}
