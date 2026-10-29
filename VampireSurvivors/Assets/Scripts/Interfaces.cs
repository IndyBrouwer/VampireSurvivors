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
    public void Initialize(EnemyData enemyData, Transform playerPos, Animator animator);
    public void Move();
    public void UpdateAnimation();
}

public interface IDroppable
{
    public void Drop();
}

public interface ICollectable
{
    public void Initialize(float droppedAmount);
    public void Collect(GameObject player);
}

public interface ILevelable
{
    public void LevelUp(int waveNumber);
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

    public void SetValues(float damageMultiplyer, float sizeMultiplyer, float speedMultiplyer, int projectileIncrease, int increaseHits);
}

public interface IClickable
{
    public void OnButtonClick();
}

public interface IUpgradable
{
    public bool CanUpgrade();
    public int GetCurrentLevel();
    public void Upgrade();
}