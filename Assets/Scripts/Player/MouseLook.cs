using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    GameObject player;
    GameObject cam;
    float xRot;

    public float mouseSensetivity = 700;
    public float maxXrot = 90f;
    public float minXrot = -90f;

    void Start()
    {
        xRot = 0;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = this.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

        xRot -= y;
        xRot = Mathf.Clamp(xRot, minXrot, maxXrot);

        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        player.transform.Rotate(new Vector3(0,x,0));
    }
}
