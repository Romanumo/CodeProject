using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetNamed : WinCondition
{
    [SerializeField] private GameObject target;
    [SerializeField] private string name;

    public override bool Check()
    {
        if (target.name == name)
            return true;
        return false;
    }

    public override string ToObjective()
    {
        return target.name + " needed to be renamed to " + name;
    }
}
