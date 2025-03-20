using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{
    public static class Paths
    {
        public static readonly string INPUT_BINDING_SETTING = "InputSystem/InputBindingSetting";
    }


    public static class Constants
    {


    }
    public static class Enums
    {
        public enum GameState
        {
            MainMenu,
            Playing,
            Paused,
            GameOver
        }

        public enum InputAction
        {
            MoveLeft,
            MoveRight,
            Jump,
            Slide,
            Pause,
            TurnLeft,
            TurnRight,
        }

       
    }

}