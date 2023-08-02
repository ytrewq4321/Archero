using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDecorator : IWeapon
{
    public float Damage
    {
        get { return decoratedWeapon.Damage+weaponUpgrade.Damage; }
    }

    public float AttackRate
    {
        get { return decoratedWeapon.AttackRate + weaponUpgrade.AttackRate; }
    }

    public int MagazineSize
    {
        get { return decoratedWeapon.MagazineSize + weaponUpgrade.MagazineSize; }
    }

    public float ReloadTime
    {
        get { return decoratedWeapon.ReloadTime - weaponUpgrade.ReloadTime; }
    }

    public int ProjectileCount
    {
        get { return decoratedWeapon.ProjectileCount + weaponUpgrade.ProjectileCount; }
    }

    private readonly IWeapon decoratedWeapon;
    private readonly WeaponUpgrade weaponUpgrade;

    public WeaponDecorator(IWeapon weapon, WeaponUpgrade upgrade)
    {
        decoratedWeapon = weapon;
        weaponUpgrade = upgrade;
    }
}
