using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensetivity = 700;
    public float maxXrot = 90f;
    public float minXrot = -90f;

    private GameObject player;
    private GameObject cam;
    private float xRot = 0;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = this.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float normalizedDeltaTime = Time.deltaTime * 60.0f;

        float x = Input.GetAxis("Mouse X") * mouseSensetivity * normalizedDeltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensetivity * normalizedDeltaTime;

        xRot -= y;
        xRot = Mathf.Clamp(xRot, minXrot, maxXrot);

        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        player.transform.Rotate(new Vector3(0,x,0));
    }
}
