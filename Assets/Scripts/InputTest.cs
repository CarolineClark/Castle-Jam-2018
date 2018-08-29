using System.Collections;
using System.Collections.Generic;

public class InputTest : IInput
    {
        private List<float> inputs;
        private List<bool> jumps;
        public float RIGHT = 1f;
        public float LEFT = -1f;
        public float JUMP = 2f;

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

        public void SetLeftPressed()
        {
            inputs.Add(RIGHT);
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
                inputs.Remove(0);
                return input;
            }
            return defaultValue;
        }
    }