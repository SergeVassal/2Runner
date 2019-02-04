using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : VirtualInput
{
    private void AddButton(string name)
    {
        // if we have not registered this button yet,  add it
        ProtoCrossPlatformInputManager.RegisterVirtualButton(new ProtoCrossPlatformInputManager.VirtualButton(name));
    }

    private void AddAxes(string name)
    {
        // if we have not registered this button yet,  add it
        ProtoCrossPlatformInputManager.RegisterVirtualAxis(new ProtoCrossPlatformInputManager.VirtualAxis(name));
    }



    public override Vector3 MousePosition()
    {
        return virtualMousePosition;
    }



    public override void SetButtonDown(string name)
    {
        if (!virtualButtons.ContainsKey(name))
        {
            AddButton(name);
        }
        virtualButtons[name].Pressed();
    }

    public override void SetButtonUp(string name)
    {
        if (!virtualButtons.ContainsKey(name))
        {
            AddButton(name);
        }
        virtualButtons[name].Released();
    }

    public override bool GetButton(string name)
    {
        if (virtualButtons.ContainsKey(name))
        {
            return virtualButtons[name].GetButton;
        }

        AddButton(name);
        return virtualButtons[name].GetButton;
    }

    public override bool GetButtonDown(string name)
    {
        if (virtualButtons.ContainsKey(name))
        {
            return virtualButtons[name].GetButtonDown;
        }

        AddButton(name);
        return virtualButtons[name].GetButtonDown;
    }

    public override bool GetButtonUp(string name)
    {
        if (virtualButtons.ContainsKey(name))
        {
            return virtualButtons[name].GetButtonUp;
        }

        AddButton(name);
        return virtualButtons[name].GetButtonUp;
    } 
    


    public override void SetAxis(string name, float value)
    {
        if (!virtualAxes.ContainsKey(name))
        {
            AddAxes(name);
        }
        virtualAxes[name].Update(value);
    }

    public override void SetAxisPositive(string name)
    {
        if (!virtualAxes.ContainsKey(name))
        {
            AddAxes(name);
        }
        virtualAxes[name].Update(1f);
    }

    public override void SetAxisNegative(string name)
    {
        if (!virtualAxes.ContainsKey(name))
        {
            AddAxes(name);
        }
        virtualAxes[name].Update(-1f);
    }

    public override void SetAxisZero(string name)
    {
        if (!virtualAxes.ContainsKey(name))
        {
            AddAxes(name);
        }
        virtualAxes[name].Update(0f);
    }

    public override float GetAxis(string name, bool raw)
    {
        if (!virtualAxes.ContainsKey(name))
        {
            AddAxes(name);
        }
        return virtualAxes[name].GetValue;
    }
}
