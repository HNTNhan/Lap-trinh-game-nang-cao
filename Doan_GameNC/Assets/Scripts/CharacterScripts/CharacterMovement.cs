using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private float forwardMoveSpeed = 7f;
    [SerializeField]
    private float backwardMoveSpeed = 3;
    [SerializeField]
    private float turnSpeed = 150f;
    [SerializeField]
    private float maxStamina = 5;
    private float curStamina;
    private bool isRecoverStamina = false;

    private float moveSpeed;
    private GameObject landmark;
    private int check;
    private float checkRotate;
    private float rotate;
    public GameObject victory;
    public GameObject CMvcam;

    public event Action<float> OnStaminaPctChanged = delegate { };

    private void Awake()
    {
        landmark = GameObject.FindGameObjectWithTag("Landmark");
        check = 0;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        checkRotate = Input.GetAxis("Mouse X");
        rotate = Input.GetAxis("Mouse X");

        curStamina = maxStamina;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, landmark.GetComponent<Transform>().position) <= 10)
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                victory.SetActive(true);
                CMvcam.SetActive(false);
                PauseMenu.GameIsPause = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0f;
                Debug.Log("Victory");
            }
        }
        
       
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
        
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (vertical > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !isRecoverStamina)
            {
                moveSpeed = forwardMoveSpeed * 1.8f;
                vertical *= 2f;
                animator.SetLayerWeight(1, 0);
                animator.SetBool("IsRunning", true);
            }
            else
            {
                moveSpeed = forwardMoveSpeed;
                animator.SetLayerWeight(1, 1);
                animator.SetBool("IsRunning", false);
            }

        }
        else
        {
            animator.SetBool("IsRunning", false);
            moveSpeed = backwardMoveSpeed;
        }


        var movement = new Vector3(horizontal, 0, vertical);

        animator.SetFloat("SpeedY", vertical);
        animator.SetFloat("SpeedX", horizontal);

        if(Input.GetAxis("Mouse X") != checkRotate)
        {
            var horizontalMouse = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, horizontalMouse * turnSpeed * Time.deltaTime);
            rotate = Input.GetAxis("Mouse X");
            checkRotate = Input.GetAxis("Mouse X");
        }
        else
        {
            if (Input.GetKey(KeyCode.L))
            {
                transform.Rotate(Vector3.up, 1f);
            }
            if (Input.GetKey(KeyCode.J))
            {
                transform.Rotate(Vector3.up, -1f);
            }
        }

        characterController.SimpleMove(transform.forward * moveSpeed * vertical);
        characterController.SimpleMove(transform.right * moveSpeed * horizontal);

        if (animator.GetBool("IsRunning"))
        {
            curStamina -= Time.deltaTime;

            if (curStamina < 0)
            {
                isRecoverStamina = true;
                curStamina = 0;
                animator.SetBool("IsRunning", false);
            }
        }
        else if (curStamina < maxStamina)
        {
            curStamina += Time.deltaTime/2;
            if (curStamina >= maxStamina / 3)
                isRecoverStamina = false;
        }
        float curStaminaPct = (float)curStamina / (float)maxStamina;
        OnStaminaPctChanged(curStaminaPct);
    }
}
