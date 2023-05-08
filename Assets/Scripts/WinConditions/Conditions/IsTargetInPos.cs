using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetInPos : WinCondition
{
    [SerializeField] private GameObject target;
    [SerializeField] private Transform point;
    [SerializeField] private float radius;

    public override bool Check()
    {
        float distance = (target.transform.position - point.position).magnitude;
        if (distance < radius)
            return true;
        return false;
    }

    public override string ToObjective()
    {
        return "Get " + target.name + " object to the destination";
    }
}
