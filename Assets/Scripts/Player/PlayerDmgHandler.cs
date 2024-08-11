using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmgHandler : MonoBehaviour
{
    private PlayerStats playerStats;
    private Collider2D playerCollider;
    private SimpleFlash simpleFlash;
    private Animator animator;

    [SerializeField]
    private LayerMask ignoreMask;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == 7)
        {
            playerStats.loseHP();
            playerStats.GetComponent<Rigidbody2D>().AddForce((-collision.collider.gameObject.transform.position + transform.position) * 10f
                ,ForceMode2D.Impulse);
            playerCollider.excludeLayers += ignoreMask;
            animator.SetBool("IsHurt",true);
            simpleFlash.Flash();
            StartCoroutine(Invencibility());
        }
        else if(collision.collider.gameObject.layer == 8)
        {
            Debug.Log("yummi");
        }
    }

    void Start ()
    {
        playerStats = GetComponent<PlayerStats>();
        playerCollider = GetComponent<Collider2D>();
        simpleFlash = GetComponent<SimpleFlash>();
        animator = GetComponent<Animator>();
    }

    IEnumerator Invencibility()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("IsHurt",false);
        yield return new WaitForSeconds(2.25f);
        playerCollider.excludeLayers -= ignoreMask;
    }

}
