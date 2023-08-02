using UnityEngine;

public class ProjectileSpawner 
{
    private readonly Projectile.Pool projectilePool;

    public ProjectileSpawner(Projectile.Pool projectilePool)
    {
        this.projectilePool = projectilePool;
    }

    public void Spawn(Vector3 spawnPos,Vector3 direction,float damage,ProjectileOwner owner)
    {
        projectilePool.Spawn(spawnPos,direction,damage,owner);
    }
}
