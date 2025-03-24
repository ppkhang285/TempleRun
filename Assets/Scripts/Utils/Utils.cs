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

        public enum SegmentBiome
        {
            Temple,
            Cliff,
            Plank,
        }

        public enum SegmentType
        {
            Straight,
            Turn_Left,
            Turn_Right,
            Flame_Tower,
            Turn_Both,
            Tree,
            Hole,
            LongHole_Start,
            LongHole_End,
            LongHole_Middle,
        }
       
    }

}