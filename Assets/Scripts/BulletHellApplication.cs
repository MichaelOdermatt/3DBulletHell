using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Link to MVC approach outline https://www.toptal.com/unity-unity3d/unity-with-mvc-how-to-level-up-your-game-development
/// </summary>
public class BulletHellApplication : MonoBehaviour
{

    public ModelContainer modelContainer;
    public ViewContainer viewContainer;
    public ControllerContainer controllerContainer;

    public void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        Object[] controllers = getAllControllers();

        foreach (IController controller in controllers)
        {
            controller.OnNotification(p_event_path, p_target, p_data);
        }
    }

    private Object[] getAllControllers()
    {
        Object[] bulletHellElements = new Object[5];

        bulletHellElements[0] = controllerContainer.cameraController;
        bulletHellElements[1] = controllerContainer.playerBulletController;
        bulletHellElements[2] = controllerContainer.playerController;
        bulletHellElements[3] = controllerContainer.basicEnemyController;
        bulletHellElements[4] = controllerContainer.enemyBulletController;

        return bulletHellElements;
    }
}
