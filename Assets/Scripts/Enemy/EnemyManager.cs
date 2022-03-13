using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public PathCreator[] pathCreators;
    public GameObject[] enemyTypes;
    public ObjectPool enemyBulletPool;
    public ObjectPool enemyPool;
    public float SpaceBetweenEnemies = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the EnemyFire event
        EnemyFire.fire += spawnBulletForEnemy;
        // Subscribe to the EnemyKilled delegate, if an enemy is killed run spawnNewWave
        KillableEnemy.enemyKilled += spawnNewWave;

        // create first wave of enemies.
        createEnemies();
    }

    // check if all enemies are dead, if so create new ones.
    private void spawnNewWave()
    {
        if (enemyPool.AreAllObjectsDeactivated())
        {
            createEnemies();
        }
    }

    // used for debugging
    private void createEnemies()
    {
        StartCoroutine(createEnemiesOnPaths(enemyPool, pathCreators, 10, 1.2f));
        createEnemiesWithWaypoints(enemyPool, waypoints);
    }

    /// <summary>
    /// *Must be started as a coroutine*
    /// Activates enemies from the enemy pool and places each one on a random path, seperated by a length of time 
    /// defined by the delay variable.
    /// </summary>
    private IEnumerator createEnemiesOnPaths(ObjectPool enemyPool, PathCreator[] paths, int numOfEnemies, float delay)
    {

        for (int i = 0; i < numOfEnemies; i++)
        {
            int randomPath = UnityEngine.Random.Range(0, paths.Length);

            GameObject enemy = activateEnemyFromPool(enemyPool, transform.position, Quaternion.identity);

            if (enemy != null)
            {
                placeEnemyOnPath(enemy, pathCreators[randomPath]);
            }

            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// Activates enemies from the enemy pool, will activate one enemy for each waypoint given.
    /// </summary>
    private void createEnemiesWithWaypoints(ObjectPool enemyPool, GameObject[] waypoints)
    {

        for (int i = 0; i < waypoints.Length; i++)
        {
            GameObject enemy = activateEnemyFromPool(enemyPool, transform.position, Quaternion.identity);

            if (enemy != null)
            {
                moveEnemyToWaypoint(enemy, waypoints[i]);
            }
        }
    }

    private void placeEnemyOnPath(GameObject enemy, PathCreator pathCreator)
    {
        MoveAlongPath moveAlongPath = enemy.GetComponent<MoveAlongPath>();
        if (moveAlongPath != null)
        {
            moveAlongPath.pathCreator = pathCreator;
        }
    }

    private void moveEnemyToWaypoint(GameObject enemy, GameObject waypoint)
    {
        MoveToWaypoint moveToWaypoint = enemy.GetComponent<MoveToWaypoint>();

        enemy.transform.position = new Vector3(waypoint.transform.position.x, 90, waypoint.transform.position.z);

        if (moveToWaypoint != null)
        {
            moveToWaypoint.Waypoint = waypoint;
        }
    }

    /// <summary>
    /// Activate a bullet at the location fo the sender object.
    /// </summary>
    private void spawnBulletForEnemy(GameObject sender)
    {
        GameObject bulletInstance = enemyBulletPool.GetPooledObject();
        if (bulletInstance != null)
        {
            bulletInstance.transform.position = sender.transform.position;
            bulletInstance.transform.rotation = sender.transform.rotation;
            bulletInstance.SetActive(true);
        }
    }

    /// <summary>
    /// Activate an enemy from the ObjectPool.
    /// </summary>
    private GameObject activateEnemyFromPool(ObjectPool enemyObjectPool, Vector3 spawnLocation, Quaternion spawnRotaion)
    {
        GameObject enemyInstance = enemyObjectPool.GetPooledObject();
        if (enemyInstance != null)
        {
            enemyInstance.transform.position = spawnLocation;
            enemyInstance.transform.rotation = spawnRotaion;
            enemyInstance.SetActive(true);

            return enemyInstance;
        }

        return null;
    }
}
