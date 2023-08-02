using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponUpgrade", menuName = "Weapon/Upgrade", order = 1)]
public class WeaponUpgrade : ScriptableObject, IWeapon
{
    [SerializeField] private float damage;
    [SerializeField] private float attackRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private int magazineSize;
    [SerializeField] private int projectileCount;
    //[SerializeField] private List<int> spawnPointsDegrees;

    public string UpgradeName;
    public string UpgradeDescription;

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

    //public List<int> SpawnPointsDegrees
    //{
    //    get { return spawnPointsDegrees; }
    //}
}