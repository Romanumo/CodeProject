using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerEntity : MonoBehaviour
{
    [SerializeField] private Compiler compiler;
    [SerializeField] private float range;
    [SerializeField] private Camera compilerCam;

    private AudioSource source;
    private Player player;
    bool programState = false;

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
            if (Time.timeScale == 0)
                Time.timeScale = 1;

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
        //compiler.CompilerState(programState);
        ChangeComputerControl(programState);
        player.ChangeControl(!programState);
    }

    private void ChangeComputerControl(bool state)
    {
        compilerCam.gameObject.SetActive(state);
    }

    private bool IsInRange()
    {
        float dist = (transform.position - player.transform.position).magnitude;
        return dist < range;
    }
}
