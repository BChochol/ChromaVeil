    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlowController : MonoBehaviour
{
    public Transform _player;
    public Vector3 _offset;
    public float _smoothingDuration = 0.125f;
    
    private Vector3 velocity = Vector3.zero;
    
    void Awake()
    {
        LevelController.RegisterCamera(this);
    }

    void Start()
    {
        _player = LevelController.GetPlayer().transform;
        transform.position = _player.position;
    }
    
    void OnDestroy()
    {
        LevelController.UnregisterCamera();
    }
    
    void Update()
    {
        Vector3 desiredPosition = _player.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, _smoothingDuration);
    }

    public void MoveCameraToPlayer()
    {
        transform.position = _player.position;
    }
    
}
