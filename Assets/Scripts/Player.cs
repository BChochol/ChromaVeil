using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _acceleration = 5f;
    private float _currentSpeed = 0f;
    [SerializeField] public float _gravityForce = 0.6f;
    [SerializeField] private Inputs inputs;
    [SerializeField] private float rotateTime = 10f;
    [SerializeField] private bool _canMove = true;
    private Vector3 _moveDir;
    [SerializeField] private GroundCollider _groundCollider;
    [SerializeField] private Animator _animator;
    
    private Vector3[] raycastOrigins = new Vector3[8];
    
    public CharacterController _characterController;
    
    public PlayerStateMachine _stateMachine;
    public PlayerMovingState _movingState;
    public PlayerFallingState _fallingState;


    private void Awake()
    {
        _stateMachine = new PlayerStateMachine();
        _movingState = new PlayerMovingState(this, _stateMachine);
        _fallingState = new PlayerFallingState(this, _stateMachine);
        
        LevelController.RegisterPlayer(gameObject);
    }
    
    private void Start()
    {
        _groundCollider = GetComponentInChildren<GroundCollider>();
        _stateMachine.Initialize(_fallingState);        
        
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f; // 45 degrees between each raycast
            Vector3 dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            raycastOrigins[i] = transform.position + Vector3.up * 0.5f + dir * 0.6f;
            
        }
        SceneChanger.Instance.ToggleShaders(false);
    }

    private void OnDestroy()
    {
        LevelController.UnregisterPlayer();
    }
    
    private void Update()
    {
        //UpdateMovement();   
        _stateMachine.CurrentPlayerState.Update();
        
        updateAnimation();
        updateSFX();
    }

    public int howManyDetectingEdge()
    {    
        int final = 0;
        foreach (Vector3 origin in raycastOrigins)
        {
            //Debug.DrawRay(transform.position + origin, transform.position + origin + Vector3.down * 500f, Color.red);
            RaycastHit hit;
            Vector3 rayStart = transform.position + origin + Vector3.up * 0.01f;
            if (Physics.Raycast(rayStart, Vector3.down, out hit, 3f))//, LayerMask.GetMask("Terrain")))
            {
                final++;
                //Debug.Log("Hit: " + hit.collider.gameObject.name + " Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
                Debug.DrawRay(rayStart, Vector3.down * 500f, Color.green);
            }
            else Debug.DrawRay(rayStart, Vector3.down * 500f, Color.red);
        }
        //Debug.Log(final);
        return final;
    }
    
    public void UpdateMovement()
    {
        _moveDir = CalculateMoveDir();
        
        if (_moveDir == Vector3.zero)
        {
            _currentSpeed = 0;
            _animator.SetFloat("PlayerSpeed", 0);
            AudioManager.Instance.StopSFXWheels();
            return;
        }

        Vector3 gravityDir = new();
        if (howManyDetectingEdge() == 0)
        {
            //Debug.Log("Falling!");
            gravityDir = Vector3.zero;
            Debug.Log(howManyDetectingEdge());
        }
        else
        {
            gravityDir = Physics.gravity * Time.deltaTime * _gravityForce;
        }
         _characterController.Move(gravityDir);
        
        if (_currentSpeed < _moveSpeed) _currentSpeed += _acceleration * Time.deltaTime;
        
        float moveDistance = _currentSpeed * Time.deltaTime;
        
        var rotateDirection = new Vector3(transform.forward.x, 0, transform.forward.z);
        transform.forward = Vector3.Slerp(rotateDirection, _moveDir, Time.deltaTime * rotateTime);
        

        _characterController.Move(_moveDir * moveDistance + gravityDir);
        
    }
    
    public Vector3 CalculateMoveDir()
    {
        if (!_canMove) return Vector3.zero;
        Vector2 movementInputVector = inputs.GetMovementVectorNormalized();
        float Xdir = movementInputVector.x;
        float Ydir = movementInputVector.y;

        Vector3 moveDir = new Vector3(Xdir, 0, Ydir);
        
        moveDir = new Vector3(moveDir.x, moveDir.y, moveDir.z*Mathf.Cos(Mathf.Deg2Rad * 30));
        moveDir = Quaternion.Euler(0, 45, 0) * moveDir;
        
        return moveDir.normalized;
    }

    private void updateAnimation()
    {
        if (_currentSpeed > 0)
        {
            _animator.SetFloat("PlayerSpeed", 20);
        }
        else
        {
            //stop the animation
            _animator.SetFloat("PlayerSpeed", _currentSpeed);
        }
    }

    private void updateSFX()
    {
        if (_currentSpeed > 0)
        {
            AudioManager.Instance.PlaySFXWheels();
        }
        else
        {
            AudioManager.Instance.StopSFXWheels();
        }
    }
}
