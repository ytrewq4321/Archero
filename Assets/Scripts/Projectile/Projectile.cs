using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed;
    private ProjectileOwner owner;
    private Vector3 direction;
    private float damage;
    private Pool pool;

    [Inject]
    public void Construct(Pool pool)
    {
        this.pool = pool;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void DealDamage(Collider other)
    {
        pool.Despawn(this);
        other.GetComponent<IDamagable>().TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(owner == ProjectileOwner.PLAYER && other.CompareTag("Enemy"))
        {
            DealDamage(other);
        }      
        else if(owner == ProjectileOwner.ENEMY && other.CompareTag("Player"))
        {
            DealDamage(other);
        }
        else if(other.CompareTag("Wall"))
        {
            pool.Despawn(this);
        }
    }

    public class Pool : MonoMemoryPool<Vector3,Vector3,float,ProjectileOwner, Projectile>
    {
        protected override void Reinitialize(Vector3 spawnPoint, Vector3 direction,float damage,ProjectileOwner owner,Projectile item)
        {
            item.transform.position = spawnPoint;
            item.direction = direction;
            item.damage = damage;
            item.owner = owner;
        }
    }
}

public enum ProjectileOwner
{
    PLAYER,
    ENEMY
}
