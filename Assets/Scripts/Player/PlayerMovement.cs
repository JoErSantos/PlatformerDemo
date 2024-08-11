using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D playerCollider;

    [SerializeField]
    private LayerMask enemyMask;
    [SerializeField]
    private LayerMask interactableMask;
    [SerializeField]
    private Transform interactBox;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 5f;
    [SerializeField]
    private float rollForce = 2f;
    private bool isOnGround;
    private float xVelocity = 0f;

    private bool lookingToRigth = true;
    private bool isRollAvailable = true;
    
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Movement",xVelocity);
        animator.SetFloat("VerticalSpeed", playerRigidBody.velocity.x);
        animator.SetBool("IsOnGround",isOnGround);
        if(playerRigidBody.velocity.y <= 0.2f && playerRigidBody.velocity.y >= -0.2)
            isOnGround = true;
        else
            isOnGround = false;
    }

    public void Movement(Vector2 direction)
    {
        if(direction.x == -1f)
        {
            spriteRenderer.flipX = true;
            lookingToRigth = false;
            interactBox.localPosition = new Vector3(-0.4f,-0.1f,0f);
        }
        else if(direction.x == 1f)
        {
            spriteRenderer.flipX = false;
            lookingToRigth = true;
            interactBox.localPosition = new Vector3(0.4f,-0.1f,0f);
        }
        Vector2 dirVect = new Vector2(direction.x,0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3)dirVect, speed * Time.deltaTime);
        xVelocity = dirVect.x * speed;
    }

    public void Jump()
    {
        if(isOnGround)
        {
            Vector3 JumpForce = new Vector3 (0,jumpHeight,0);
            playerRigidBody.AddForce(jumpHeight * JumpForce, ForceMode2D.Impulse);
        }
    }

    public void Roll()
    {
        if(isOnGround && isRollAvailable)
        {
            float multiplier = 1f;
            if(!lookingToRigth)
                multiplier = -1f;
            Vector3 rollForceVect = new Vector3 (rollForce,0,0);
            playerRigidBody.AddForce(speed * rollForceVect * multiplier, ForceMode2D.Impulse);
            playerCollider.excludeLayers += enemyMask;
            playerCollider.excludeLayers += interactableMask;
            animator.SetBool("IsRolling",true);
            isRollAvailable = false;
            StartCoroutine(WaitForRoll());
        }
    }

    IEnumerator WaitForRoll()
    {
        yield return new WaitForSeconds(0.25f);
        Debug.Log("DONE");
        animator.SetBool("IsRolling",false);
        playerCollider.excludeLayers -= enemyMask;
        playerCollider.excludeLayers += interactableMask;
        yield return new WaitForSeconds(2.75f);
        isRollAvailable = true;
    }
}
