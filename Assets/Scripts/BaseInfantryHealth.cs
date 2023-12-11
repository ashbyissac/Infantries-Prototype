using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInfantryHealth : MonoBehaviour
{
    [SerializeField] InfantryHealthBar infantryHealthBar;
    [SerializeField] Transform originPoint;
    [SerializeField] int maxHealth;
    [SerializeField] int damage;

    int health;
    public int Health => health;

    void OnEnable()
    {
        health = maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RedSword")
        {
            DamageInfantry();
        }
        else if (other.gameObject.tag == "BlueSword")
        {
            DamageInfantry();
        }
    }

    void DamageInfantry()
    {
        health -= damage;
        infantryHealthBar.UpdateEnemyHealthBar(health);
        if (health < 1)
        {
            health = 0;
            var infantryController = gameObject.GetComponent<BaseInfantryController>();
            infantryController.IsTargetLocked = false;
            infantryController.IsAlive = false;
            infantryController.InfantryAnimator.SetBool("isAttack", false);
            infantryController.InfantryAnimator.SetTrigger("isDead");
            StartCoroutine(AfterDeathAction(infantryController));
        }
    }

    IEnumerator AfterDeathAction(BaseInfantryController hitInfantrysController)
    {
        yield return new WaitForSeconds(2f);
        hitInfantrysController.gameObject.SetActive(false);
        hitInfantrysController.InfantryAnimator.ResetTrigger("isDead");
        if (hitInfantrysController is BlueInfantryController)
            BlueInfantryObjectPool.Instance.EnqueueInfantry(BlueInfantryObjectPool.Instance.blueInfantryPoolQueue, hitInfantrysController.gameObject);
        else
            RedInfantryObjectPool.Instance.EnqueueInfantry(RedInfantryObjectPool.Instance.redInfantryPoolQueue, hitInfantrysController.gameObject);
    }

}
