using System.Collections;
using System.Collections.Generic;

public class InputTest : IInput
{
    private List<float> inputs;
    private List<bool> jumps;
    private float RIGHT = 1f;
    private float LEFT = -1f;

    public InputTest() {
        inputs = new List<float>();
        jumps = new List<bool>();
    }

    public float GetAxisHorizontal()
    {
        return EatList<float>(inputs, 0);
    }

    public bool Jump()
    {
        return EatList<bool>(jumps, false);
    }

    public void SetRightPressed()
    {
        inputs.Add(RIGHT);
    }

    public void SetRightPressed(int number) {
        for (int i = 0; i < number; i++) {
            SetRightPressed();    
        }    
    }

    public void SetLeftPressed()
    {
        inputs.Add(LEFT);
    }

    public void SetLeftPressed(int number)
    {
        for (int i = 0; i < number; i++)
        {
            SetLeftPressed();
        }
    }

    public void SetJumpPressed()
    {
        jumps.Add(true);
    }

    public void SetJumpNotPressed()
    {
        jumps.Add(false);
    }

    private T EatList<T>(List<T> list, T defaultValue)
    {
        if (!list.Count.Equals(0))
        {
            T input = list[0];
            inputs.RemoveAt(0);
            return input;
        }
        return defaultValue;
    }
}