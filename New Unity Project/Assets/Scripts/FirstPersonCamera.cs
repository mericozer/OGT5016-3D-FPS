using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float rotationAmount = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationAmount -= mouseY;
        rotationAmount = Mathf.Clamp(rotationAmount, -90f, 90f); //limit the mouse angles of Y axis

        transform.localRotation = Quaternion.Euler(rotationAmount, 0f, 0f);
        
        player.Rotate(Vector3.up * mouseX); //make player rotate with camera
    }
}
