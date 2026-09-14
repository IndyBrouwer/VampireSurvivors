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

public interface ICollectable
{
    public void Pickup();
}