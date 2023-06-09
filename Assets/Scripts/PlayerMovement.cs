using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerMovement : MonoBehaviour
{

//############################
[Header("Movement")]
[SerializeField]
public bool f_Moving = false;
[SerializeField]
float f_WalkSpeed = 3.5f;
[SerializeField]
float f_SprintSpeed = 7.0f;

[Space]
[Header("Camera")]
[SerializeField]
float lookSpeed = 0.1f;
[SerializeField]
float lookHeight = 3.0f;
[SerializeField]
float lookDistance = 4.0f;
[SerializeField]
float forwardLookOffset = 1.0f;
[SerializeField]
float upLookOffset = 1.0f;
//############################

    CharacterController r_CC;
    PlayerInput r_PI;
    Camera r_CAM;

    public Vector2 m_Movement = Vector3.zero;

    void Awake()
    {
        r_CC = GetComponent<CharacterController>();
        r_CAM = GetComponentInChildren<Camera>();
        r_PI = GetComponent<PlayerInput>();

    }

    void OnEnable()
    {
        r_PI.ActivateInput();
        r_PI.actions["Move"].performed += MovePresed;
        r_PI.actions["Move"].canceled += MoveReleased;
    }


    void OnDisable()
    {
        r_PI.DeactivateInput();

        r_PI.actions["Move"].performed -= MovePresed;
        r_PI.actions["Move"].canceled -= MoveReleased;
    }

    void Update()
    {
        if(f_Moving) move();
    }

    void MovePresed(InputAction.CallbackContext context)
    {
        m_Movement = context.ReadValue<Vector2>();
    }

    void MoveReleased(InputAction.CallbackContext context)
    {
        m_Movement = Vector3.zero;
    }

    void move()
    {
        if(m_Movement == Vector2.zero) 
        {
            r_CC.SimpleMove(Vector3.zero);
            return;
        }
    
        float walkSpeed = Input.GetKey(KeyCode.LeftShift) ? f_WalkSpeed * 2.0f : f_WalkSpeed;
        Vector3 mov = r_CAM.transform.right * m_Movement.x + r_CAM.transform.forward * m_Movement.y;
        mov = new Vector3(mov.x, 0, mov.z).normalized * walkSpeed;
        r_CC.SimpleMove(mov);
    }

}