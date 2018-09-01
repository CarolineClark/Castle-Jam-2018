﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWrapper
{
    public IInput inputClass { get; set; }
    public InputWrapper()
    {
        inputClass = new InputReal();
    }

    public float GetAxisHorizontal()
    {
        return inputClass.GetAxisHorizontal();
    }

    public bool IsDoorKeyPressed() {
        return inputClass.IsDoorKeyPressed();
    }

    public bool Jump()
    {
        return inputClass.Jump();
    }
}