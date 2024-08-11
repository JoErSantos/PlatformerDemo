using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerControls.MovementActions movement;
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerInteractor playerInteractor;
    void Awake()
    {
        playerControls = new PlayerControls();
        movement =  playerControls.Movement;
        playerMovement = GetComponent<PlayerMovement>();
        movement.Jump.performed += ctx => playerMovement.Jump();
        movement.Roll.performed += ctx => playerMovement.Roll();
        movement.Grab.performed += ctx => playerInteractor.setsetIsObjectGrabbed();
        movement.Throw.performed += ctx => playerInteractor.throwInteractable();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerMovement.Movement(movement.SideMovement.ReadValue<Vector2>());
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }
}
