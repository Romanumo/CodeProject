using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTheory : MonoBehaviour
{
    [SerializeField] private GameObject windowTheory;
    [SerializeField] private GameObject wall;
    [SerializeField] private float range;

    private Player player;
    bool programState = false;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (IsInRange() && Input.GetKeyDown(KeyCode.Escape))
        {
            programState = !programState;
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
