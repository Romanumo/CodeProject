using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private GameObject compCam;

    private Program program;
    private AudioSource source;
    private Player player;
    private bool programState = false;
            
    void Awake()
    {
        program = GetComponent<Program>();
        source = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        //TODO: Sounds into a sepaarte computerSoundManager
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
        compCam.SetActive(programState);
        player.ChangeControl(!programState);
        Cursor.lockState = (programState) ? CursorLockMode.None : CursorLockMode.Locked;

        if(program != null)
            program.ProgramState(programState);
    }

    private bool IsInRange()
    {
        float dist = (transform.position - player.transform.position).magnitude;
        return dist < range;
    }
}
