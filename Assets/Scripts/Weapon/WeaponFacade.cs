using System.Collections;
using UnityEngine;
using Zenject;

public class WeaponFacade : MonoBehaviour
{
    public IWeapon weapon;
    public WeaponConfig weaponConfig;
    public Transform SpawnPoint;
    private ProjectileSpawner projectileSpawner;
    private Coroutine coroutine;

    private bool isShooting;

    [Inject]
    public void Construct(ProjectileSpawner projectileSpawner)
    {
        this.projectileSpawner = projectileSpawner;
    }

    public void Initialize()
    {
        weapon = new Weapon(weaponConfig);
    }

    public void Upgrade(WeaponUpgrade upgrade)
    {
        weapon = new WeaponDecorator(weapon, upgrade);
    }

    public void Shoot(Vector3 position,float damage,float attackRate)
    {
        if (!isShooting)
        {
            damage += weapon.Damage;
            attackRate += weapon.AttackRate;
            coroutine = StartCoroutine(ShootCoroutine(position,damage, attackRate));
        }      
    }
     
    private IEnumerator ShootCoroutine(Vector3 position, float damage,float attackRate)
    {
        isShooting = true;

        var dir = (position - transform.position).normalized;
        projectileSpawner.Spawn(SpawnPoint.position, dir, damage,ProjectileOwner.PLAYER);
        yield return new WaitForSeconds(1f/attackRate);

        isShooting = false;
    }

    public void OnDisable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }    
        isShooting = false;
    }
}
