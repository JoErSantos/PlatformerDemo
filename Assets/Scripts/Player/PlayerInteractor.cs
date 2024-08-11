using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]
    private float throwForce = 10f;
    private GameObject Interactable;
    private bool IsObjectGrabbed = false;
    private bool wasObjectRealesed = true;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
            Interactable = collider.gameObject;
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if(Interactable == collider.gameObject)
        {
            Interactable.GetComponent<Collider2D>().isTrigger = false;
            Interactable.GetComponent<Rigidbody2D>().gravityScale = 9.2f;
            Interactable = null;
        }
    }

    void FixedUpdate()
    {
        grabInteractable();
    }

    private void grabInteractable()
    {
        if(IsObjectGrabbed && Interactable != null)
        {
            Interactable.GetComponent<Collider2D>().isTrigger = true;
            Interactable.GetComponent<Rigidbody2D>().gravityScale = 0;
            Interactable.transform.position = transform.position;
            wasObjectRealesed = false;
        }
        else if(!wasObjectRealesed && Interactable != null)
            realeseInteractable();
        else
            IsObjectGrabbed = false;
    }

    private void realeseInteractable()
    {
        Interactable.GetComponent<Collider2D>().isTrigger = false;
        Interactable.GetComponent<Rigidbody2D>().gravityScale = 9.2f;
        if(transform.localPosition.x < 0)
            Interactable.transform.position = new Vector3(Interactable.transform.position.x - 0.5f,Interactable.transform.position.y,Interactable.transform.position.z);
        else if(transform.localPosition.x > 0)
            Interactable.transform.position = new Vector3(Interactable.transform.position.x + 0.5f,Interactable.transform.position.y,Interactable.transform.position.z);
        wasObjectRealesed = true;
    }

    public void throwInteractable()
    {
        if(Interactable != null && IsObjectGrabbed)
        {
            realeseInteractable();
            Vector3 throwVect = new Vector3(transform.localPosition.x * 10f,5f,0);
            Interactable.GetComponent<Rigidbody2D>().AddForce(throwVect * throwForce,ForceMode2D.Impulse);
            IsObjectGrabbed = false;
        }
    }

    public void setsetIsObjectGrabbed()
    {
        IsObjectGrabbed = !IsObjectGrabbed;
    }
}
