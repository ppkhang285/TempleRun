using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static InputBindingSetting;
using static Utils.Enums;
public class InputManager
{
    private static InputManager _instance;
    public static InputManager Instance => _instance ?? (_instance = new InputManager());


    private InputBindingSetting inputBindingSetting;
    private Dictionary<InputAction, InputBinding> bindingMap;

    public InputManager()
    {

        LoadBindings();
       


    }
    private void LoadBindings()
    {
        bindingMap = new Dictionary<InputAction, InputBinding>();
        var settings = Resources.Load<InputBindingSetting>(Paths.INPUT_BINDING_SETTING);
        if (settings == null)
        {
            Debug.LogError("InputBindingSetting not found");
            return;
        }

        foreach (var binding in settings.bindings)
        {
            if (!bindingMap.ContainsKey(binding.action))
                bindingMap.Add(binding.action, binding);
        }

    }
    public bool GetInput(InputAction action)
    {
        if (!bindingMap.ContainsKey(action))
            return false;

        var binding = bindingMap[action];


        // Keyboard
        if (!binding.isMouse)
        {
            bool keyPressed = binding.key != KeyCode.None && Input.GetKeyDown(binding.key);
            return keyPressed;
        }
        else
        {
            return HandleMoveWithMouse(action);
        }
        
    }

    private bool HandleMoveWithMouse(InputAction action)
    {
        // Mouse
        bool mouseSwipe = false;
        float mouseX = Input.GetAxis("Mouse X");
        float swipeThreshold = 0.5f;


        if (action == InputAction.MoveLeft && mouseX < -swipeThreshold)
            mouseSwipe = true;
        if (action == InputAction.MoveRight && mouseX > swipeThreshold)
            mouseSwipe = true;

        return mouseSwipe;
    }
}
