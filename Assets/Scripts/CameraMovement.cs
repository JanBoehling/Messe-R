using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform PlayerCamera;
    private float XRotation = 0f;

    private float MouseX = 0;
    private float MouseY = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance == null)
        {
            Debug.LogError("Game Manger is null");
            return;
        }

        MouseX = Input.GetAxis("Mouse X") * GameManager.Instance.CameraSensibility * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * GameManager.Instance.CameraSensibility * Time.deltaTime;

        XRotation -= MouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
        PlayerCamera.Rotate(Vector3.up * MouseX);

    }
}
