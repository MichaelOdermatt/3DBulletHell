using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : BulletHellElement 
{
    public CharacterController characterController;
    public GameObject characterModel; 

    private void OnCollisionEnter(Collision collision)
    {
        app.Notify(BulletHellNotification.PlayerControllerOnCollision , this, collision); 
    }
}
