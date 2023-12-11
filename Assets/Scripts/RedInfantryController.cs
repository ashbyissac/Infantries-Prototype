using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedInfantryController : BaseInfantryController
{

    void Update()
    {
        FindClosestTarget(SpawnManager.Instance.BlueInfantryControllers);
        if (closestInfantryTarget)
            EngagePlayer(closestInfantryTarget);
    }
}
