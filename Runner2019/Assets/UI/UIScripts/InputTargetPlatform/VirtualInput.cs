using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VirtualInput 
{
    // Dictionary to store all the virtual axes which have been registered by CrossPlatformInputManager
    protected Dictionary<string, ProtoCrossPlatformInputManager.VirtualAxis> virtualAxes =
            new Dictionary<string, ProtoCrossPlatformInputManager.VirtualAxis>();

    // Dictionary to store all the virtual buttons which have been registered by CrossPlatformInputManager
    protected Dictionary<string, ProtoCrossPlatformInputManager.VirtualButton> virtualButtons =
            new Dictionary<string, ProtoCrossPlatformInputManager.VirtualButton>();

    // list of the axis and button names that have been flagged to always use a virtual axis or button
    protected List<string> alwaysUseVirtual = new List<string>();



    
    public bool AxisExists(string name)
    {
        return virtualAxes.ContainsKey(name);
    }

    public bool ButtonExists(string name)
    {
        return virtualButtons.ContainsKey(name);
    }

    // returns a reference to a named virtual axis if it exists otherwise null
    public ProtoCrossPlatformInputManager.VirtualAxis VirtualAxisReference(string name)
    {
        return virtualAxes.ContainsKey(name) ? virtualAxes[name] : null;
    }



    public void RegisterVirtualAxis(ProtoCrossPlatformInputManager.VirtualAxis axis)
    {
        // check if we already have an axis with that name and log and error if we do
        if (virtualAxes.ContainsKey(axis.name))
        {
            Debug.LogError("There is already a virtual axis named " + axis.name + " registered.");
        }
        else
        {
            // add any new axes
            virtualAxes.Add(axis.name, axis);

            // if we dont want to match with the input manager setting then revert to always using virtual
            if (!axis.matchWithInputManager)
            {
                alwaysUseVirtual.Add(axis.name);
            }
        }
    }

    public void RegisterVirtualButton(ProtoCrossPlatformInputManager.VirtualButton button)
    {
        // check if already have a buttin with that name and log an error if we do
        if (virtualButtons.ContainsKey(button.name))
        {
            Debug.LogError("There is already a virtual button named " + button.name + " registered.");
        }
        else
        {
            // add any new buttons
            virtualButtons.Add(button.name, button);

            // if we dont want to match to the input manager then always use a virtual axis
            if (!button.matchWithInputManager)
            {
                alwaysUseVirtual.Add(button.name);
            }
        }
    }

    public void UnRegisterVirtualAxis(string name)
    {
        // if we have an axis with that name then remove it from our dictionary of registered axes
        if (virtualAxes.ContainsKey(name))
        {
            virtualAxes.Remove(name);
        }
    }

    public void UnRegisterVirtualButton(string name)
    {
        // if we have a button with this name then remove it from our dictionary of registered buttons
        if (virtualButtons.ContainsKey(name))
        {
            virtualButtons.Remove(name);
        }
    }



    public Vector3 virtualMousePosition { get; private set; }

    public void SetVirtualMousePositionX(float f)
    {
        virtualMousePosition = new Vector3(f, virtualMousePosition.y, virtualMousePosition.z);
    }

    public void SetVirtualMousePositionY(float f)
    {
        virtualMousePosition = new Vector3(virtualMousePosition.x, f, virtualMousePosition.z);
    }

    public void SetVirtualMousePositionZ(float f)
    {
        virtualMousePosition = new Vector3(virtualMousePosition.x, virtualMousePosition.y, f);
    }

    public abstract Vector3 MousePosition();  

    

    public abstract void SetButtonDown(string name);

    public abstract void SetButtonUp(string name);

    public abstract bool GetButton(string name);

    public abstract bool GetButtonDown(string name);

    public abstract bool GetButtonUp(string name);
           


    public abstract void SetAxis(string name, float value);

    public abstract void SetAxisPositive(string name);

    public abstract void SetAxisNegative(string name);

    public abstract void SetAxisZero(string name);

    public abstract float GetAxis(string name, bool raw);   
}
