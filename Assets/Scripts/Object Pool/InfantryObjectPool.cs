using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InfantryObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public int poolSize;
        public GameObject prefab;
    }

    protected Transform infantryObjectPoolParent;

    public Queue<GameObject> blueInfantryPoolQueue = new Queue<GameObject>();
    public Queue<GameObject> redInfantryPoolQueue = new Queue<GameObject>();

    protected abstract void PopulateInfantryPool();
    public abstract GameObject EnableInfantryInPool(Vector3 startPoint);

    public void EnqueueInfantry(Queue<GameObject> infantryPoolQueue, GameObject infantryInstance) => infantryPoolQueue.Enqueue(infantryInstance);

    public GameObject DequeueInfantry(Queue<GameObject> infantryPoolQueue) => infantryPoolQueue.Dequeue();

    protected void SetInfantry(GameObject activeInfantry, Vector3 startPoint)
    {
        SetInfantryPositionAndRotation(activeInfantry, startPoint);
        activeInfantry.SetActive(true);
    }

    protected void SetInfantryPositionAndRotation(GameObject activeInfantry, Vector3 startPoint)
    {
        activeInfantry.transform.position = startPoint;
        activeInfantry.transform.rotation = Quaternion.identity;
    }
}