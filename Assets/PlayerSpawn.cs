using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Awake()
    {
        LevelController.RegisterPlayerSpawner(this);
    }
    
    public void OnDestroy()
    {
        LevelController.UnregisterPlayerSpawner();
    }
    
    public void MovePlayerToSpawner(GameObject player)
    {
        player.transform.position = transform.position;
    }

}
