using UnityEngine;

public class EnemyManagerController : BulletHellElement 
{
    private BasicEnemyController basicEnemyController;

    private void Awake()
    {
        basicEnemyController = app.controllerContainer.basicEnemyController;

    }

    private void Start()
    {
        GameObject basicEnemy = basicEnemyController.GetPooledObject();
        basicEnemy.transform.position = new Vector3(32, 25, 32);
        basicEnemy.SetActive(true);
    }
}
