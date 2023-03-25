using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WinCondition : MonoBehaviour
{
    public abstract string ToObjective();
    public abstract bool Check();
}