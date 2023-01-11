using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    //public Camera GameCamera;
    private static ThirdPersonController instance;
    public static ThirdPersonController Instance => instance;

    public float playerSpeed = 8.5f;
    [SerializeField] private float JumpForce = 1.0f;

    private CharacterController m_Controller;
    private Animator m_Animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    private float input_x;
    private float input_z;

    private void Start()
    {
        instance = this;
        m_Controller = gameObject.GetComponent<CharacterController>();
        m_Animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        InputLogic();
        GravityCaculate();
    }
    private void LateUpdate()
    {
        MoveLogic();
    }
    void InputLogic()
    {
        input_x = Input.GetAxis("Horizontal");
        input_z = Input.GetAxis("Vertical");
        groundedPlayer = m_Controller.isGrounded;
    }
    private void MoveLogic()
    {
        //transform.Translate(moveDirection * playerSpeed * Time.deltaTime, Space.World);
        Vector3 moveDirection = new Vector3(input_x, 0, input_z);
        transform.LookAt(transform.position + moveDirection);
        m_Controller.Move(moveDirection * Time.deltaTime * playerSpeed);
        m_Animator.SetFloat("MovementX", input_x);
        m_Animator.SetFloat("MovementZ", input_z);
        m_Controller.Move(playerVelocity * Time.deltaTime);
    }
    private void CheckGroundLogic()
    {
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.5f;
        }
        //Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(JumpForce * -3.0f * gravityValue);
            m_Animator.SetTrigger("Jump");
        }
    }
    private void GravityCaculate()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
    }
}
