using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Mirror.BouncyCastle.Asn1.Esf;
using System;
using System.Diagnostics;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]

public class GrabThrow : MonoBehaviour
{
    public float grabDistance = 3f;  
    public float throwForce = 5f;    
    private GameObject grabbedObject = null;  
    public bool isGrabbing = false;  
    public Transform holdPosition;  

    void Update()
    {
        //Calls TryGrabObject function to try to throw objects 
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isGrabbing)
            {
                ThrowObject();
            }
            else
            {
                
            }
            
        }
        //Calls TryGrabObject function to try to grab objects 
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrabbing)
            {
                TryGrabObject();
            }

        }

        if (isGrabbing && grabbedObject != null)                                
        {
            
            grabbedObject.transform.position = holdPosition.position;       //Sets the objects position to the hold position assigned to the character
        }
    }

    void TryGrabObject()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, grabDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Grabbable"))                      //If the object's collider is within the hold position and they are tagged as 'Grabbable' then the object's physics are turned off
            {
                GrabObject(collider.gameObject);
                return;  
            }
        }
    }

    void GrabObject(GameObject obj)
    {
        grabbedObject = obj;

        
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();       //checks for rigid body and makes sure the object is kinematic
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        
        Collider col = grabbedObject.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;                                          //turns off the objects colliders
        }

        isGrabbing = true;
    }

    void ThrowObject()
    {
        if (grabbedObject != null)
        {
            
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.angularDamping = 0f;
                rb.linearDamping = 0f;
                rb.isKinematic = false;                                              //turns kinematic off and gives force to the object releasing it from the hold position
                rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grabbedObject.transform.position += new Vector3(0, 0.1f, 0);
                
            }

            
            Collider col = grabbedObject.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;                                    //enables the colliders back on
            }

            
            grabbedObject = null;
            isGrabbing = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, grabDistance);        //helps to see the grab distance's area of effect
    }

    private static object GetDebuggerDisplay()
    {
        throw new NotImplementedException();                    //implemented debugger
    }
}
