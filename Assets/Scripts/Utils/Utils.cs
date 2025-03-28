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

        public readonly static float CHARACTER_MASS = 2.0f;
        public readonly static float CHARACTER_VERTICAL_VELOCITY = 20.0f;
        public readonly static float CHARACTER_JUMP_FORCE = 50.0f;
        public readonly static float GRAVITY = 70.0f;

        public static readonly Dictionary<Enums.Direction, Vector3> DIRECTION_VECTOR = new Dictionary<Enums.Direction, Vector3>
        {
            { Enums.Direction.FORWARD, Vector3.right },
            { Enums.Direction.BACKWARD, Vector3.left },
            { Enums.Direction.LEFT, Vector3.forward },
            { Enums.Direction.RIGHT, Vector3.back }
        };

        public static readonly Dictionary<Enums.Direction, Quaternion> ROTATION_VECTOR = new Dictionary<Enums.Direction, Quaternion>
        {
            { Enums.Direction.FORWARD, Quaternion.Euler(0, 0, 0) },
            { Enums.Direction.BACKWARD, Quaternion.Euler(0, 180, 0) },
            { Enums.Direction.LEFT, Quaternion.Euler(0, -90, 0) },
            { Enums.Direction.RIGHT, Quaternion.Euler(0, 90, 0) }
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
        public enum Direction
        {
            FORWARD,
            BACKWARD,
            LEFT,
            RIGHT

        }

    }
    public static class Methods
    {
        public static Enums.Direction TurnDirection(Enums.Direction currDirect, bool isTurnLeft)
        {
            switch (currDirect)
            {
                case Enums.Direction.FORWARD:
                    currDirect = isTurnLeft ? Enums.Direction.LEFT : Enums.Direction.RIGHT;
                    break;

                case Enums.Direction.BACKWARD:
                    currDirect = isTurnLeft ? Enums.Direction.RIGHT : Enums.Direction.LEFT;
                    break;
                case Enums.Direction.LEFT:
                    currDirect = isTurnLeft ? Enums.Direction.BACKWARD : Enums.Direction.FORWARD;
                    break;
                case Enums.Direction.RIGHT:
                    currDirect = isTurnLeft ? Enums.Direction.FORWARD : Enums.Direction.BACKWARD;
                    break;
            }

            return currDirect;
        }
    }
   

}