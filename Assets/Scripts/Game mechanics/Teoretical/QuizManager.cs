using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class QuizManager : MonoBehaviour
{
    protected Action onWin;
    protected Action onLose;

    public void ModWin(Action winAction) => onWin += winAction;
    public void ModLose(Action loseAction) => onLose += loseAction;
}
