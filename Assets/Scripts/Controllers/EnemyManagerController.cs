using UnityEngine;

public class EnemyManagerController : BulletHellElement , IController 
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

    public void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
    }
}
