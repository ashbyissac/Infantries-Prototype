using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedInfantryObjectPool : InfantryObjectPool
{
    [SerializeField] Pool redInfantryPoolData;
    public static RedInfantryObjectPool Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);

        infantryObjectPoolParent = GameObject.FindGameObjectWithTag("RedInfantries").transform;
        PopulateInfantryPool();
    }

    protected override void PopulateInfantryPool()
    {
        for (int i = 0; i < redInfantryPoolData.poolSize; i++)
        {
            GameObject infantryInstance = Instantiate(redInfantryPoolData.prefab);
            infantryInstance.SetActive(false);
            EnqueueInfantry(redInfantryPoolQueue, infantryInstance);
            infantryInstance.transform.parent = infantryObjectPoolParent;
        }
    }

    public override GameObject EnableInfantryInPool(Vector3 startPoint)
    {
        GameObject activeInfantry;
        if (redInfantryPoolQueue.Count > 0)
        {
            GameObject dequeuedInfantry = DequeueInfantry(redInfantryPoolQueue);
            activeInfantry = dequeuedInfantry;
        }
        else
        {
            GameObject newInstance = Instantiate(redInfantryPoolData.prefab);
            activeInfantry = newInstance;
            activeInfantry.transform.parent = infantryObjectPoolParent;
        }

        SetInfantry(activeInfantry, startPoint);
        return activeInfantry;
    }
}
