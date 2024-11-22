using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPanController : MonoBehaviour
{   
    private Vector2 _userInput;
    public float Speed { get; private set; } = 5;

    void Update()
    {
        transform.position +=  
        (Vector3)(transform.rotation *
                  Camera.main.transform.localRotation *
                  _userInput *
                  Speed *
                  Time.deltaTime);
    }
    public void Pan(Vector2 input) => _userInput = input;
    
    public void Pan(InputAction.CallbackContext callbackContext)
    {
        System.Action<Vector2> action = callbackContext.phase switch
        {
            InputActionPhase.Performed => Pan,
            InputActionPhase.Canceled => Pan,
            _ => NoOp,
        };
        action.Invoke(callbackContext.ReadValue<Vector2>());
        static void NoOp(Vector2 _)  { }
    }
}