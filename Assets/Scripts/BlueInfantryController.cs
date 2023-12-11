using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueInfantryController : BaseInfantryController
{

    void Update()
    {
        FindClosestTarget(SpawnManager.Instance.RedInfantryControllers);
        if (closestInfantryTarget)
            EngagePlayer(closestInfantryTarget);
    }
}
