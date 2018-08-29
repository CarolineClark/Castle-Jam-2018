using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWrapper
{
    private IInput inputClass { get; set; }
    public InputWrapper()
    {
        inputClass = new InputReal();
    }

    public float GetAxisHorizontal()
    {
        return inputClass.GetAxisHorizontal();
    }

    public bool Jump()
    {
        return inputClass.Jump();
    }
}
