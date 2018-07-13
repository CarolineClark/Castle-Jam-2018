using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static string HORIZONTAL_AXIS = "Horizontal";
    public static string VERTICAL_AXIS = "Vertical";

    public static string PLAYER_ANIMATION_LEFT = "left";
    public static string PLAYER_ANIMATION_RIGHT = "right";
    public static string PLAYER_ANIMATION_IDLE = "PlayerIdle";
    public static string PLAYER_ANIMATION_SPEED = "speed";

    public static string PLAYER_TAG = "Player";

    // events
    public static string CHECKPOINT_EVENT_KEY = "CheckpointEvent";
    public static string FALLING_OBJECT_HIT_EVENT = "FallingObjectEvent";
    public static string PLAYER_DIED_EVENT = "player died";

    public static string JUMP = "Jump";

    public static int PLAYER_LAYER = 8;
    public static int GROUND_LAYER = 9;
    public static int SIGN_LAYER = 10;
    public static int PLATFORM_LAYER = 11;

    public static string RESTART_GAME = "Restart game";

    public static string STOP_SIGNS_FALLING = "StopSignsFalling";
    public static string SET_PLAYER_SPEED = "SetPlayerSpeed";

    public static string STORMY_SKY_TO_GREY = "StormySkyToGrey";

    public static string END_GAME = "EndGame";
    public static string CAMERA_CHANGE_VIEWPORT = "CameraChangeViewport";

    public static string FADE_TO_WHITE = "fadetowhite";
}
