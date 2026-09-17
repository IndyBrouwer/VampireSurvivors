public interface IDamageable
{
    public void TakeDamage(float damage);
    public void Die();
}

public interface IEnemyBehaviour
{
    public void Initialize(EnemyData enemyData);
    public void Move();
}

public interface IDroppable
{
    public void Drop();
}

public interface ICollectable
{
    public void Pickup();
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