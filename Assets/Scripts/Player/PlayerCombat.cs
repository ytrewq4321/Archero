using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private EnemyHandler enemyHandler;
    [SerializeField] private List<WeaponFacade> weapons;
    [SerializeField] private WeaponFacade EquipedWeapon;
    private InputManager inputManager;
    private int indexWeapon;
    private float switchingWeaponsDelay = 0.4f;
    private bool isWeaponSwitching;

    [Inject]
    public void Constructor(EnemyHandler enemyHandler, InputManager inputManager)
    {
        this.enemyHandler = enemyHandler;
        this.inputManager = inputManager;
    }

    private void Start()
    {
        inputManager.WeaponSwitched += OnWeaponSwitched;
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        foreach (var weapon in weapons)
        {
            weapon.Initialize();
        }
    }

    private void OnWeaponSwitched()
    {
        if(!isWeaponSwitching)
        {
            StartCoroutine(SwitchWeaponCoroutine());
        }      
    }

    private void SwitchWeapon()
    {
        EquipedWeapon.gameObject.SetActive(false);

        indexWeapon++;
        if(indexWeapon==weapons.Count)
        {
            indexWeapon = 0;
        }     
        EquipedWeapon = weapons[indexWeapon];
        EquipedWeapon.gameObject.SetActive(true);
    }

    public void Attack(Vector3 targetPostion, float damage, float attackRate)
    {
        EquipedWeapon.Shoot(targetPostion,damage, attackRate);
    }

    public Transform FindNearestEnemy(Vector3 playerPosition)
    {
        float sqrDistance = float.MaxValue;
        Transform target = null;

        foreach (var enemy in enemyHandler.Enemies)
        {
            if(enemy!=null)
            {
                var newDistance = (enemy.transform.position - playerPosition).sqrMagnitude;
                if (newDistance < sqrDistance)
                {
                    sqrDistance = newDistance;
                    target = enemy.transform;
                }
            }          
        }
        return target;
    }

    private void OnDestroy()
    {
        inputManager.WeaponSwitched -= OnWeaponSwitched;
    }

    private IEnumerator SwitchWeaponCoroutine()
    {
        isWeaponSwitching = true;

        yield return new WaitForSeconds(switchingWeaponsDelay);
        SwitchWeapon();

        isWeaponSwitching = false;
    }
}
