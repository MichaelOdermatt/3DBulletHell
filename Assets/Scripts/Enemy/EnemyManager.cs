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

        createEnemies();
    }

    // used for debugging
    private void createEnemies()
    {
        GameObject enemy = activateEnemyFromPool(enemyPool, transform.position, Quaternion.identity);

        moveEnemyToWaypoint(enemy, waypoints[0]);

        //StartCoroutine(createEnemiesOnPaths(pathCreators, 10, 1.2f));
    }

    /// <summary>
    /// *Must be started as a coroutine*
    /// Takes in two int arrays, one for enemy indexes and another for path indexes, the script places 
    /// each enemy id on its respective path id, for example: if enemyIndexes[0] = 1 and pathIndexes[0] = 3
    /// the script will place an enemy of index 1 in the enemies array, on the path of index 3 in the pathCreators array.
    /// </summary>
    private IEnumerator createEnemiesOnPaths(PathCreator[] paths, int numOfEnemies ,float delay)
    {

        for (int i = 0; i < numOfEnemies; i++)
        {
            int randomPath = UnityEngine.Random.Range(0, paths.Length);

            GameObject enemy = activateEnemyFromPool(enemyPool, transform.position, Quaternion.identity);

            if (enemy != null)
            {
                placeEnemyOnPath(enemy, pathCreators[randomPath], 0);
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private void placeEnemyOnPath(GameObject enemy, PathCreator pathCreator, float offsetOnPath)
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
