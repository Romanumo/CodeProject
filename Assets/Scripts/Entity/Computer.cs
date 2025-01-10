using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Computer : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected GameObject cam;

    protected AudioSource source;
    protected Player player;
    protected bool programState = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (IsInRange() && Input.GetKeyDown(KeyCode.Escape))
        {
            programState = !programState;

            if (programState)
                source.Play();
            else
            {
                source.time = 0;
                source.Stop();
            }

            GeneralFunctions.BlackScreenBoth(0.5f, ChangeGameState);
        }
    }

    private void ChangeGameState()
    {
        cam.SetActive(programState);
        player.ChangeControl(!programState);
        Cursor.lockState = (programState) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private bool IsInRange()
    {
        float dist = (transform.position - player.transform.position).magnitude;
        return dist < range;
    }
}
