using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapon : IWeapon
{
    public float Damage
    {
        get { return weaponConfig.Damage; }
    }

    public float AttackRate
    {
        get { return weaponConfig.AttackRate; }
    }

    public int MagazineSize
    {
        get { return weaponConfig.MagazineSize; }
    }

    public float ReloadTime
    {
        get { return weaponConfig.ReloadTime; }
    }

    public int ProjectileCount
    {
        get { return weaponConfig.ProjectileCount; }
    }

    private readonly WeaponConfig weaponConfig;

    public Weapon(WeaponConfig weaponConfig)
    {
        this.weaponConfig = weaponConfig;
    }
}
