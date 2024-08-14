using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public bool givesLive = false;
    public int points = 100;
    public bool isRespawnable = true;

    public bool wasConsumed = false;

    private Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    public void consume()
    {
        animator.SetBool("wasConsumed",true);
        StartCoroutine(WaitForDespawn());
    }

    IEnumerator WaitForDespawn()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    public void PlayRespawnAnimation()
    {
        animator.SetBool("wasConsumed",false);
    }
}
