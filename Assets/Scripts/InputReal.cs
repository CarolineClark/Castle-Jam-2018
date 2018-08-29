using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReal : IInput
{
    public float GetAxisHorizontal()
    {
        return Input.GetAxis(Constants.HORIZONTAL_AXIS);
    }

    public bool Jump()
    {
        return Input.GetButtonDown(Constants.JUMP);
    }
}
