using System.Collections.Generic;
using System;

public abstract class VirtualInput 
{
    protected Dictionary<string, CrossPlatformInputManager.VirtualAxis> virtualAxes =
        new Dictionary<string, CrossPlatformInputManager.VirtualAxis>();

    protected Dictionary<string, CrossPlatformInputManager.VirtualButton> virtualButtons =
        new Dictionary<string, CrossPlatformInputManager.VirtualButton>();



    public CrossPlatformInputManager.VirtualAxis VirtualAxisReference(string name)
    {
        return AxisExists(name) ? virtualAxes[name] : null;
    }

    public bool AxisExists(string name)
    {
        return virtualAxes.ContainsKey(name);
    }

    public bool ButtonExists(string name)
    {
        return virtualButtons.ContainsKey(name);
    }

    public void RegisterVirtualAxis(CrossPlatformInputManager.VirtualAxis newVirtualAxis)
    {
        if (AxisExists(newVirtualAxis.Name))
        {
            throw new Exception("There is already a virtual axis named " + newVirtualAxis.Name + " registered.");
        }
        virtualAxes.Add(newVirtualAxis.Name, newVirtualAxis);
    }

    public void RegisterVirtualButton(CrossPlatformInputManager.VirtualButton newVirtualButton)
    {
        if (ButtonExists(newVirtualButton.Name))
        {
            throw new Exception("There is already a virtual button named " + newVirtualButton.Name + " registered.");
        }
        virtualButtons.Add(newVirtualButton.Name, newVirtualButton);
    }

    public void UnRegisterAxis(string name)
    {
        if (AxisExists(name))
        {
            throw new Exception("There's no such axis registered!");
        }
        virtualAxes.Remove(name);
    }

    public void UnRegisterButton(string name)
    {
        if (!ButtonExists(name))
        {
            throw new Exception("There's no such button registered!");
        }
        virtualButtons.Remove(name);
    }


    public abstract void SetAxis(string name, float value);

    public abstract void SetAxisPositive(string name);

    public abstract void SetAxisNegative(string name);

    public abstract void SetAxisZero(string name);

    public abstract float GetAxis(string name,bool raw);


    public abstract void SetButtonDown(string name);

    public abstract void SetButtonUp(string name);

    public abstract bool GetButtonDown(string name);

    public abstract bool GetButtonUp(string name);

    public abstract bool GetButton(string name);
}
