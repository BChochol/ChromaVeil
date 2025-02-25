using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    
    private static GameObject _player;
    private static CameraFlowController _camera;
    private static PlayerSpawn _playerSpawner;
    private static Canvas _fadeCanvas;
 
    public static void RegisterPlayer(GameObject player)
    {
        if (_player == null)
            _player = player;
        else Debug.LogError("More than one player registered, make sure that only one player is present on the scene.");
    }
    
    public static void UnregisterPlayer()
    {
        if(_player)
        _player = null;
    }
    
    public static void RegisterCamera(CameraFlowController camera)
    {
        _camera = camera;
    }
    
    public static void UnregisterCamera()
    {
        _camera = null;
    }
    
    public static void RegisterPlayerSpawner(PlayerSpawn playerSpawner)
    {
        if (_playerSpawner == null)
            _playerSpawner = playerSpawner;
        else Debug.LogError("More than one player spawner registered, make sure that only one player spawner is present on the scene.");
    }
    
    public static void UnregisterPlayerSpawner()
    {
        if(_playerSpawner)
        _playerSpawner = null;
    }
    
    public static void RegisterFadeCanvas(Canvas fadeCanvas)
    {
        if (_fadeCanvas == null)
            _fadeCanvas = fadeCanvas;
    }
    
    public static void UnregisterFadeCanvas()
    {
        if(_fadeCanvas)
        _fadeCanvas = null;
    }

    void Start()
    {
        _playerSpawner.MovePlayerToSpawner(_player);
        _camera.MoveCameraToPlayer();
    }

    public static GameObject GetPlayer()
    {
        return _player;
    }
}
