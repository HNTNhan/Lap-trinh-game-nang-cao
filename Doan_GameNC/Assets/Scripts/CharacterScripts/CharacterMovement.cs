using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private float forwardMoveSpeed = 7.5f;
    [SerializeField]
    private float backwardMoveSpeed = 3;
    [SerializeField]
    private float turnSpeed = 150f;

    private int check;

    private void Awake()
    {
        check = 0;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(PlayerPrefs.GetString("Load") == "true")
        {
            check++;
            if (check == 2)
            {
                check = 0;
                PlayerPrefs.SetString("Load", "false");
            }
            return;
        }
        if (animator.GetBool("Die")) return;
        var horizontalMouse = Input.GetAxis("Mouse X");
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var movement = new Vector3(horizontal, 0, vertical);

        animator.SetFloat("SpeedY", vertical);
        animator.SetFloat("SpeedX", horizontal);

        transform.Rotate(Vector3.up, horizontalMouse * turnSpeed * Time.deltaTime);

        if(vertical != 0)
        {
            float moveSpeed = vertical > 0 ? forwardMoveSpeed : backwardMoveSpeed;

            characterController.SimpleMove(transform.forward * moveSpeed * vertical);
            characterController.SimpleMove(transform.right * moveSpeed * horizontal);
        }
        characterController.SimpleMove(transform.right * backwardMoveSpeed * horizontal);
    }
}
