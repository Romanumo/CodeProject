using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEntity : PracticeEntity
{
    public float reloadTime = 10;
    public List<GameObject> enemies;

    private List<GameObject> targets;
    private bool hasReloaded = true;

    private void Start()
    {
        targets = new List<GameObject>();
        CollisionKiller[] killers = FindObjectsOfType<CollisionKiller>();
        foreach (CollisionKiller killer in killers)
            enemies.Add(killer.gameObject);
    }

    private void Update()
    {
        if (targets.Count > 0)
            ShootEntity(targets[0]);
    }

    public void AddTarget(GameObject target)
    {
        targets.Add(target);
    }

    private void ShootEntity(GameObject entity)
    {
        if (!hasReloaded)
            return;

        targets.RemoveAt(0);
        Destroy(entity);
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        hasReloaded = false;
        yield return new WaitForSeconds(reloadTime);
        hasReloaded = true;
    }
}
