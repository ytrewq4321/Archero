using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    float Damage { get; }

    float AttackRate { get; }

    int MagazineSize {get; }

    float ReloadTime { get; }

    int ProjectileCount { get; }
}
