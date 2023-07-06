using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerEntity : MonoBehaviour
{
    [SerializeField] private CompilerProgram compiler;
    [SerializeField] private float range;
    [SerializeField] private Camera compilerCam;

    private AudioSource source;
    private Player player;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (IsInRange() && Input.GetKeyDown(KeyCode.Escape))
        {
            bool state = (GameManager.instance.state == GameState.Computer);
            GameManager.instance.ChangeGameState((!state) ? GameState.Computer : GameState.Player);

            ComputerSoundsState(state);

            GameManager.BlackScreenBoth(0.5f, () => ComputerState(state));
        }
    }

    private void ComputerSoundsState(bool state)
    {
        if (state)
            source.Play();
        else
        {
            source.time = 0;
            source.Stop();
        }
    }

    private void ComputerState(bool state)
    {
        compiler.CompilerState(state);
        ChangeComputerControl(state);
        player.ChangeControl(!state);
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
