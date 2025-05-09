using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.Enums;



[CreateAssetMenu(fileName = "InputBindingSetting", menuName = "InputSystem/InputBindingSetting", order = 1)]
public class InputBindingSetting : ScriptableObject
{
    [System.Serializable]
    public class InputBinding
    {
        public InputAction action;
        public KeyCode key;
        public bool isMouse;
    }

    public List<InputBinding> bindings = new List<InputBinding>();
}