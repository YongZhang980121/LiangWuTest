using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyPrefab; // Enemy prefab to spawn
    public Vector2 spawnRangeX = new Vector2(-10, 10); // X range for spawning
    public Vector2 spawnRangeY = new Vector2(-10, 10); // Y range for spawning
    public GameObject player; // Reference to the player GameObject
    public float safeDistance = 5f; // Safe distance from the player
    public GameObject slimePrefab;
    public IObjectPool<GameObject> slimePool;

    private IObjectPool<Enemy> enemyPool;

    public GameObject scorePrefab;
    private IObjectPool<GameObject> scorePool;

    private void Awake()
    {
        Global.enemyManager = this;
        // Initialize the object pool
        enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(enemyPrefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: false,
            defaultCapacity: 25,
            maxSize: 50
        );
        
        slimePool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(slimePrefab),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 40);
        
        scorePool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(scorePrefab),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 40);
    }
    
    public GameObject GetScore()
    {
        return scorePool.Get();
    }

    public void ReturnScore(GameObject score)
    {
        scorePool.Release(score);
    }
    
    public GameObject GetSlime()
    {
        return slimePool.Get();
    }

    public void ReturnSlime(GameObject slime)
    {
        slimePool.Release(slime);
    }

    public void TrySpawningEnemy()
    {
        SpawnEnemy();
        DOVirtual.DelayedCall((60f / (Global.numOfEnemyPerMinute + Global.chaos*2)), () =>
        {
            TrySpawningEnemy();
        }).SetUpdate(false);
    }

    public void SpawnEnemy()
    {
        var enemy = enemyPool.Get();
        enemy.transform.SetParent(Global.battleManager.enemyHolder);
        Vector2 spawnPosition;
        bool isPositionSafe;

        int attempts = 0;
        int maxAttempts = 10; // Limit the number of attempts to avoid long processing

        do
        {
            // Generate a random position within the spawn range
            float xPosition = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float yPosition = Random.Range(spawnRangeY.x, spawnRangeY.y);
            spawnPosition = new Vector2(xPosition, yPosition);

            // Check if the position is safe
            isPositionSafe = Vector2.Distance(spawnPosition, player.transform.position) >= safeDistance;

            attempts++;
        } while (!isPositionSafe && attempts < maxAttempts);

        // If a safe position isn't found, ignore the safe distance and use the last generated position
        enemy.transform.position = spawnPosition;
    }
    
    public void ReturnEnemyToPool(Enemy enemy)
    {
        enemyPool.Release(enemy);
    }
}
