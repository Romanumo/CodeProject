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

    protected virtual void OnWin()
    {
        if (wall != null) 
            wall.SetActive(false);

        hardware.GetSoundManager().CorrectSound();
    }

    protected virtual void OnLose()
    {
        hardware.GetSoundManager().IncorrectSound();
    }
}
