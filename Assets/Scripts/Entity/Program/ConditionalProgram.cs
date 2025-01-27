using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For now conditional program just opens doors, but it can be expanded later
public class ConditionalProgram : Program
{
    [SerializeField] private GameObject wall;

    public override void ProgramState(bool state)
    {
        QuizManager manager = GameObject.FindObjectOfType<QuizManager>();
        manager.ModWin(OnWin);
        manager.ModLose(OnLose);
        Destroy(this);
    }

    //TODO: Add sounds
    protected virtual void OnWin()
    {
        Debug.Log("Won round");
        wall.SetActive(false);
    }

    protected virtual void OnLose()
    {
        Debug.Log("Lost round");
    }
}
