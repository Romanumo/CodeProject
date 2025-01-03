using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeEntity : PracticeEntity
{
    private Vector3 proportions;

    private new void Start()
    {
        base.Start();
        proportions = transform.localScale.normalized;
    }

    public void ChangeSize(float size)
    {
        transform.localScale = proportions * size;
    }
}
