using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript;

public class CameraMovement : MonoBehaviour
{

    public Camera cam;
    public Transform camTransform;
    public GameObject centerPole;
    float secsHoldingRight = 0f;
    Vector3 lastMousePosition;
    public float rotationSensitivity = 1f;
    public float movingSensitivity = 10f;

    void Start()
    {
        camTransform = cam.gameObject.transform;
        centerPole = GameObject.Find("centerOfScreen");
    }

    void Update()
    {

        MoveCamera();

        //Rotate camera
        if (Input.GetMouseButton(1))
        {
            secsHoldingRight += Time.deltaTime;

            if (secsHoldingRight > 0.2f)
            {
                if (Input.mousePosition.x > lastMousePosition.x)
                {
                    RotateCamera(rotationSensitivity * Time.deltaTime * (Input.mousePosition.x - lastMousePosition.x));
                }
                else if (Input.mousePosition.x < lastMousePosition.x)
                {
                    RotateCamera(rotationSensitivity * Time.deltaTime * (Input.mousePosition.x - lastMousePosition.x));
                }
            }
        }
        else
        {
            secsHoldingRight = 0f;
        }
        lastMousePosition = Input.mousePosition;
    }

    void RotateCamera(float rotationAmount)
    {
        cam.transform.RotateAround(Vector3.zero, Vector3.up, rotationAmount * 10f);
    }

    void MoveCamera()
    {
        if (Input.mousePosition.y >= Screen.height * 0.95)
        {
            cam.transform.position += Vector3.forward * (Time.deltaTime * movingSensitivity);
        }
        else if (Input.mousePosition.y <= Screen.height * 0.05)
        {
            cam.transform.position += Vector3.back * (Time.deltaTime * movingSensitivity);
        }

        if (Input.mousePosition.x >= Screen.width * 0.95)
        {
            cam.transform.position += Vector3.right * (Time.deltaTime * movingSensitivity);
        }
        else if (Input.mousePosition.x <= Screen.width * 0.05)
        {
            cam.transform.position += Vector3.left * (Time.deltaTime * movingSensitivity);
        }
    }
}
