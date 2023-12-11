using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class BaseInfantryController : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected new Rigidbody rigidbody;

    protected STATES InfantryStates;
    protected Transform closestInfantryTarget;
    protected bool isTargetLocked = false;

    public bool IsTargetLocked { get => isTargetLocked; set => isTargetLocked = true; }
    public Transform ClosestInfantryTarget => closestInfantryTarget;
    public Animator InfantryAnimator => animator;
    public Rigidbody Rigidbody => rigidbody;

    bool isAlive;
    public bool IsAlive
    {
        get => isAlive;
        set => isAlive = value;
    }

    public enum STATES
    {
        Idle = 1,
        Run = 2,
        Attack = 4,
        Death = 3
    }

    protected void EngagePlayer(Transform infantryInstance)
    {
        if (infantryInstance && infantryInstance.GetComponent<BaseInfantryController>().IsAlive && isTargetLocked)
        {
            navMeshAgent.SetDestination(infantryInstance.transform.localPosition);
            transform.LookAt(infantryInstance.localPosition);
            if (GetDistanceToInfantry(infantryInstance) > navMeshAgent.stoppingDistance && !InfantryStates.HasFlag(STATES.Run))
            {
                animator.SetBool("isRun", true);
                animator.SetBool("isAttack", false);
                InfantryStates |= STATES.Run;
                InfantryStates &= ~STATES.Attack;
            }
            else if (GetDistanceToInfantry(infantryInstance) <= navMeshAgent.stoppingDistance && !InfantryStates.HasFlag(STATES.Attack))
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isAttack", true);
                InfantryStates |= STATES.Attack;
                InfantryStates &= ~STATES.Run;
            }
        }
        else
        {
            InfantryAnimator.SetBool("isAttack", false);
            InfantryAnimator.SetBool("isRun", false);
            InfantryStates &= ~InfantryStates;
            isTargetLocked = false;
        }
    }

    protected void FindClosestTarget(List<Transform> infantryControllers)
    {
        if (infantryControllers.Count > 0)
        {
            infantryControllers = infantryControllers.Where(infantryController => infantryController.gameObject.activeInHierarchy).ToList();
            float distance = Mathf.Infinity;
            for (int i = 0; i < infantryControllers.Count; i++)
            {
                float distanceToInfantry = Vector3.Distance(infantryControllers[i].transform.position, transform.position);
                if (distanceToInfantry <= distance)
                {
                    isTargetLocked = true;
                    distance = distanceToInfantry;
                    closestInfantryTarget = infantryControllers[i];
                }
            }
        }
    }

    protected float GetDistanceToInfantry(Transform infantry) => Vector3.Distance(infantry.position, transform.position);

    void OnEnable()
    {
        IsAlive = true;
    }
}
