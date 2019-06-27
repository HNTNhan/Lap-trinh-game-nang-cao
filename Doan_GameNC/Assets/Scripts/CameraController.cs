using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 3.0f)]
    private float sensitivity = 0.8f;

    private CinemachineComposer composer;

    private float check;
    private float rotateCameraV;
    private float rotateCameraH;

    // Start is called before the first frame update
    private void Start()
    {
        composer = GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineComposer>();
        check = Input.GetAxis("Mouse Y");
        rotateCameraV = Input.GetAxis("Mouse Y");
        rotateCameraV = Input.GetAxis("Mouse X");
    }

    // Update is called once per frame
    void Update()
    {
        if (check != Input.GetAxis("Mouse Y"))
        {
            float vertical = Input.GetAxis("Mouse Y") * sensitivity;
            composer.m_TrackedObjectOffset.y += vertical;
            composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -10, 10);
            check = Input.GetAxis("Mouse Y");
        }
        else
        {

            if (Input.GetKey(KeyCode.I))
            {
                composer.m_TrackedObjectOffset.y += 0.05f;
            }
            if (Input.GetKey(KeyCode.K))
            {
                composer.m_TrackedObjectOffset.y -= 0.05f;
            }
            //if (Input.GetKey(KeyCode.L))
            //{
            //    composer.m_TrackedObjectOffset.x += 0.05f;
            //}
            //if (Input.GetKey(KeyCode.J))
            //{
            //    composer.m_TrackedObjectOffset.x -= 0.05f;
            //}

            composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -10, 10);
           // composer.m_TrackedObjectOffset.x = Mathf.Clamp(composer.m_TrackedObjectOffset.x, -10, 10);
        }
    }
}
