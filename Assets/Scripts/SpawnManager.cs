using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform blueInfantPrefab;
    [SerializeField] Transform redInfantPrefab;

    List<Transform> blueInfantryControllers = new List<Transform>();
    List<Transform> redInfantryControllers = new List<Transform>();

    public List<Transform> BlueInfantryControllers => blueInfantryControllers;
    public List<Transform> RedInfantryControllers => redInfantryControllers;

    public static SpawnManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SpawnInfantries(true);

        if (Input.GetMouseButtonDown(1))
            SpawnInfantries(false);
    }

    void SpawnInfantries(bool isBlueInfant)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform infantInstance = null;
            if (isBlueInfant)
            {
                infantInstance = BlueInfantryObjectPool.Instance.EnableInfantryInPool(hit.point).transform;
                blueInfantryControllers.Add(infantInstance);
            }
            else
            {
                infantInstance = RedInfantryObjectPool.Instance.EnableInfantryInPool(hit.point).transform;
                redInfantryControllers.Add(infantInstance);
            }
        }
    }
}
