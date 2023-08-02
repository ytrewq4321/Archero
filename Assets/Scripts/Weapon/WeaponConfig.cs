using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponConfig",menuName = "Weapon/Config", order = 1)]
public class WeaponConfig : ScriptableObject, IWeapon
{
    [SerializeField] private float damage;
    [SerializeField] private float attackRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private int magazineSize;
    [SerializeField] private int projectileCount;

    public string weaponName;
    public GameObject weaponPrefab;
    public string weaponDescription;

    public float Damage
    {
        get { return damage; }
    }

    public float AttackRate
    {
        get { return attackRate; }
    }

    public int MagazineSize
    {
        get { return magazineSize; }
    }

    public float ReloadTime
    {
        get { return reloadTime; }
    }

    public int ProjectileCount
    {
        get { return projectileCount; }
    }
}