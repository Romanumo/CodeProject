using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTheory : MonoBehaviour
{
    [SerializeField] private GameObject windowTheory;
    [SerializeField] private GameObject wall;
    [SerializeField] private float range;

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

    public void OpenTheWay()
    {
        wall.SetActive(false);
    }

    private void ChangeGameState()
    {
        player.ChangeControl(!programState);
        windowTheory.SetActive(programState);
    }

    private bool IsInRange()
    {
        float dist = (transform.position - player.transform.position).magnitude;
        return dist < range;
    }
}
