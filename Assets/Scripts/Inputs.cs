using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Inputs : MonoBehaviour
{
    public static PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new();
        _playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 movementVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        
        movementVector.Normalize();

        return movementVector;
    }
    
    public static bool IsInteracting()
    {
        return _playerInputActions.Player.Interact.triggered;
    }

    public static bool IsResetting()
    {
        return _playerInputActions.Player.Reset.triggered;
    }
    
    public static PlayerInputActions GetPlayerInputActions()
    {
        return _playerInputActions;
    }
}
