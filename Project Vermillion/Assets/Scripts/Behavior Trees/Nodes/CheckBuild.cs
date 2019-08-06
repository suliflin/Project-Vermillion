using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuild : BaseNode
{
    public override RESULTS UpdateBehavior(BaseBehaviorTree bt)
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject[] barricades = GameObject.FindGameObjectsWithTag("Barricade");
        GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
        GameObject tree = GameObject.FindGameObjectWithTag("Tree");

        float shortestDistance = Mathf.Infinity;
        GameObject closestBuild = null;

        foreach (GameObject turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(bt.transform.position, turret.transform.position);

            if (distanceToTurret < shortestDistance)
            {
                shortestDistance = distanceToTurret;
                closestBuild = turret;
            }
        }

        foreach (GameObject barricade in barricades)
        {
            float distanceToBarricade = Vector3.Distance(bt.transform.position, barricade.transform.position);

            if (distanceToBarricade < shortestDistance)
            {
                shortestDistance = distanceToBarricade;
                closestBuild = barricade;
            }
        }

        foreach (GameObject teleporter in teleporters)
        {
            float distanceToTeleporter = Vector3.Distance(bt.transform.position, teleporter.transform.position);

            if (distanceToTeleporter < shortestDistance)
            {
                shortestDistance = distanceToTeleporter;
                closestBuild = teleporter;
            }
        }

        float distanceToTree = Vector3.Distance(bt.transform.position, tree.transform.position);

        if (distanceToTree < shortestDistance)
        {
            shortestDistance = distanceToTree;
            closestBuild = tree;
        }
        //Debug.Log(shortestDistance);
        if (shortestDistance < bt.detectRange)
        {
            bt.targetBuild = closestBuild.transform;
            bt.lastPosition = bt.transform;
            current = RESULTS.SUCCEED;
            return current;
        }
        else
        {
            bt.targetBuild = null;
            bt.anim.SetBool("IsAttacking", false);
            current = RESULTS.FAILED;
            return current;
        }
    }
}