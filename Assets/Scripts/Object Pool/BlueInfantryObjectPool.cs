using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueInfantryObjectPool : InfantryObjectPool
{
    [SerializeField] Pool blueInfantryPoolData;
    public static BlueInfantryObjectPool Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);

        infantryObjectPoolParent = GameObject.FindGameObjectWithTag("BlueInfantries").transform;
        PopulateInfantryPool();
    }

    protected override void PopulateInfantryPool()
    {
        for (int i = 0; i < blueInfantryPoolData.poolSize; i++)
        {
            GameObject infantryInstance = Instantiate(blueInfantryPoolData.prefab);
            infantryInstance.SetActive(false);
            EnqueueInfantry(blueInfantryPoolQueue, infantryInstance);
            infantryInstance.transform.parent = infantryObjectPoolParent;
        }
    }

    public override GameObject EnableInfantryInPool(Vector3 startPoint)
    {
        GameObject activeInfantry;
        if (blueInfantryPoolQueue.Count > 0)
        {
            GameObject dequeuedInfantry = DequeueInfantry(blueInfantryPoolQueue);
            activeInfantry = dequeuedInfantry;
        }
        else
        {
            GameObject newInstance = Instantiate(blueInfantryPoolData.prefab);
            activeInfantry = newInstance;
            activeInfantry.transform.parent = infantryObjectPoolParent;
        }

        SetInfantry(activeInfantry, startPoint);
        return activeInfantry;
    }
}
