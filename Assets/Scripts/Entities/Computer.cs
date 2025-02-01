using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private GameObject compCam;
    [SerializeField] private ComputerSoundManager soundManager;

    private Program program;
    private Player player;

    private bool programState = false;
            
    void Awake()
    {
        TryGetComponent<Program>(out program);
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        //TODO: Sounds into a separte computerSoundManager
        if (IsInRange() && Input.GetKeyDown(KeyCode.Escape))
        {
            programState = !programState;

            soundManager.ComputerState(programState);

            if(Time.timeScale == 0) 
                GeneralFunctions.instance.ResumeTime();

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

    public ComputerSoundManager GetSoundManager() => soundManager;
}
