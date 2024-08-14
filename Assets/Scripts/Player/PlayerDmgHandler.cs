using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmgHandler : MonoBehaviour
{
    private PlayerStats playerStats;
    private Collider2D playerCollider;
    private FlashSistem flashManager;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField]
    private LayerMask ignoreMask;
    [SerializeField]
    private Transform respawnPos;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == 7)
        {
            playerStats.loseHP();
            playerStats.GetComponent<Rigidbody2D>().AddForce((-collision.collider.gameObject.transform.position + transform.position) * 10f
                ,ForceMode2D.Impulse);
            playerCollider.excludeLayers += ignoreMask;
            animator.SetBool("IsHurt",true);
            if(playerStats.getHP() == 0)
            {
                DeathHanddler();
            }
            else
            {
                flashManager.Flash(2f,4);
                StartCoroutine(Invencibility());
            }
        }
    }

    void Start ()
    {
        playerStats = GetComponent<PlayerStats>();
        playerCollider = GetComponent<Collider2D>();
        flashManager = GetComponent<FlashSistem>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void DeathHanddler()
    {
        animator.SetBool("IsPlayerDead",true);
        StartCoroutine(RespawnPlayer());
    }
    
    IEnumerator Invencibility()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("IsHurt",false);
        yield return new WaitForSeconds(2.25f);
        playerCollider.excludeLayers -= ignoreMask;
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        transform.position = respawnPos.position;
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = true;
        spriteRenderer.flipX = false;
        animator.SetBool("IsPlayerDead",false);
        animator.SetBool("IsHurt",false);
        playerStats.loseLife();
        playerCollider.excludeLayers -= ignoreMask;
    }

    //IsPlayerDead

}
