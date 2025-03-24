using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{
    public static class Paths
    {
        public static readonly string INPUT_BINDING_SETTING = "InputSystem/InputBindingSetting";
        public static readonly string DIFFICULTY_DATA = "GameSetting/DifficultData";
        public static readonly string SPAWN_CONFIG_DATA = "GameSetting/SpawnConfig";
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

        public enum MapBiome
        {
            Temple,
            Cliff,
            Plank,
        }

        public enum SegmentType
        {
            NONE,
            START,
            Straight,
            Turn_Left,
            Turn_Right,
            Turn_Both,
            Slide,
            Jump,
            NarrowLeft,
            NarrowRight,

        }
       
    }

}