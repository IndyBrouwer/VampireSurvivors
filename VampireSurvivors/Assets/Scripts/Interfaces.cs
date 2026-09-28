using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float damage);
    public void Die();
}

public interface IRegenerable
{
    public void Heal(float healAmount);
}

public interface IEnemyBehaviour
{
    public void Initialize(EnemyData enemyData, Transform playerPos);
    public void Move();
    public void UpdateAnimation(SpriteRenderer spriteRenderer);
}

public interface IDroppable
{
    public void Drop();
}

public interface ICollectable
{
    public void Initialize(float droppedAmount);
    public void Collect();
}

public interface ILevelable
{
    public void LevelUp();
    public void SetValues();
}

public interface ISpawnable
{
    public void Spawn();
}

public interface IDespawnable
{
    public void Despawn();
}

public interface IWeaponBehaviour
{
    public void Initialize(WeaponData scriptableObject, Weapon weaponBrainScript);

    public void Fire();
}