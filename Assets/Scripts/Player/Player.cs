using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera playerCam;
    private PlayerMovement movement;

    void Start()
    {
        GameManager.BlackScreen(1f, null, true);
        playerCam = transform.Find("MainCamera").GetComponent<Camera>();
        movement = GetComponent<PlayerMovement>();
    }

    public void ChangeControl(bool state)
    {
        movement.enabled = state;
        playerCam.enabled = state;
    }

    public void ChangeMovement(bool state)
    {
        movement.enabled = state;
    }
}
