using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
    public int zoom=20;
    int normal = 60;
    float smooth = 5;
    public float speed = 5.0f;
    private bool isZoomed = false;
    private float mouseWheel;
    public Transform CameraTarget;
    private Transform dummyTarget;
    private Transform cameraTransform;

    private void Awake()
    {
        transform.position=CameraTarget.transform.position;
        if (QualitySettings.vSyncCount > 0)
            Application.targetFrameRate = 60;
        else
            Application.targetFrameRate = -1;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            Input.simulateMouseWithTouches = false;

        cameraTransform = transform;
        Camera.main.transform.LookAt(CameraTarget.transform);
    }
    private void Start()
    {
        if (CameraTarget == null)
        {
            // If we don't have a target (assigned by the player, create a dummy in the center of the scene).
            dummyTarget = new GameObject("Camera Target").transform;
            CameraTarget = dummyTarget;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position=
            transform.position = new Vector3(speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, speed * Time.deltaTime, transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, speed * Time.deltaTime, transform.position.z);
        }
        mouseWheel = Input.GetAxis("Mouse ScrollWheel");
    }
}