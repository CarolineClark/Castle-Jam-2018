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

    public static string RESTART_GAME = "Restart game";

    // tags
    public static string PICKUP_TAG = "Pickup";
}
