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

        public static readonly Dictionary<Enums.MoveDirection, Vector3> DIRECTION_VECTOR = new Dictionary<Enums.MoveDirection, Vector3>
        {
            { Enums.MoveDirection.FORWARD, Vector3.right },
            { Enums.MoveDirection.BACKWARD, Vector3.left },
            { Enums.MoveDirection.LEFT, Vector3.forward },
            { Enums.MoveDirection.RIGHT, Vector3.back }
        };

        public static readonly Dictionary<Enums.MoveDirection, Quaternion> ROTATION_VECTOR = new Dictionary<Enums.MoveDirection, Quaternion>
        {
            { Enums.MoveDirection.FORWARD, Quaternion.Euler(0, 0, 0) },
            { Enums.MoveDirection.BACKWARD, Quaternion.Euler(0, 180, 0) },
            { Enums.MoveDirection.LEFT, Quaternion.Euler(0, -90, 0) },
            { Enums.MoveDirection.RIGHT, Quaternion.Euler(0, 90, 0) }
        };

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
        public enum MoveDirection
        {
            FORWARD,
            BACKWARD,
            LEFT,
            RIGHT

        }

    }

}