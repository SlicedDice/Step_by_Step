using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public float[] respawnLocation;
    public float[] respawnRotation;

    public bool beanstalkCollectible;
    public bool shipwreckCollectible;
    public bool ruinCollectible;

    public bool invertedControls;
    public bool movementByCamera;

    public PlayerData(CharacterController player)
    {
        respawnLocation = new float[3];
        respawnLocation[0] = player.returnLocation().x;
        respawnLocation[1] = player.returnLocation().y;
        respawnLocation[2] = player.returnLocation().z;

        respawnRotation = new float[4];
        respawnRotation[0] = player.returnRotation().x;
        respawnRotation[1] = player.returnRotation().y;
        respawnRotation[2] = player.returnRotation().z;
        respawnRotation[3] = player.returnRotation().w;

        beanstalkCollectible = player.foundBeanstalkCollectible;
        shipwreckCollectible = player.foundShipCollectible;
        ruinCollectible = player.foundRuinCollectible;

        invertedControls = player.invertedControls;
        movementByCamera = player.movementByCamera;
    }
}
