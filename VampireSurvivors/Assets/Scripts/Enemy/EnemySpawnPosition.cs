using UnityEngine;

public class EnemySpawnPosition : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private float distance = 22.4f;

    public Vector2 CircleSpawner(Vector2 center, int amountToCircleSpawn, int enemyIndex)
    {
        //Create a circle and devide it by the amount of enemies need to spawn, so that they are evenly spaced out
        float step = 2 * Mathf.PI / amountToCircleSpawn;
        float x = Mathf.Cos(step * enemyIndex);
        float y = Mathf.Sin(step * enemyIndex);

        Vector2 direction = new Vector2(x, y);

        //Add random offset to the distance so that enemies wont collide with each other too much while closing in on the player
        float randomOffset = Random.Range(0f, 0.1f);
        Vector2 position = center + direction * (distance + randomOffset);

        return position;
    }

    public Vector2 GetSpawnPosition(Vector2 center)
    {
        //Generate a random step
        float step = 2 * Mathf.PI * Random.Range(0f, 1f);

        //Calculate the x and y based on the random step
        Vector2 direction = new Vector2(Mathf.Cos(step), Mathf.Sin(step));

        //Inflate place to a spot off screen
        Vector2 spawnLocation = center + direction * distance;

        return spawnLocation;
    }
}