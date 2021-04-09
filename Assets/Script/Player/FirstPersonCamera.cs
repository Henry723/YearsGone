using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSens = 200f;

    public Transform player;
    public bool disabled;
    public float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        disabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            float mouseX = Input.GetAxis("Look X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Look Y") * mouseSens * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        }
    }
}
