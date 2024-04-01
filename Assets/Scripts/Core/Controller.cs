using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public UnityEvent attack;
    public UnityEvent toggleFps;
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        attack.Invoke();    
    }

    public void OnToggleFPS(InputAction.CallbackContext context)
    {
        toggleFps.Invoke();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
