using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossPlatformInputManager 
{
    public enum ActiveInputMethod
    {
        Standalone,
        Mobile
    }

    private static VirtualInput activeInput;
    private static VirtualInput mobileInput;
    private static VirtualInput standaloneInput;


    
    static CrossPlatformInputManager()
    {
        mobileInput = new MobileInput();
        standaloneInput = new StandaloneInput();
#if MOBILE_INPUT 
        activeInput = mobileInput;
#else
        activeInput=standaloneInput;
#endif
    }
        
    
    public static void ChangeActiveInputMethod(ActiveInputMethod activeInputMethod)
    {
        switch (activeInputMethod)
        {
            case ActiveInputMethod.Mobile:
                activeInput = mobileInput;
                break;
            case ActiveInputMethod.Standalone:
                activeInput = standaloneInput;
                break;                
        }
    }


    public static CrossPlatformInputManager.VirtualAxis VirtualAxisReference(string name)
    {
        return activeInput.VirtualAxisReference(name);
    }


    public static void RegisterVirtualAxis(VirtualAxis axis)
    {
        activeInput.RegisterVirtualAxis(axis);
    }

    public static void RegisterVirtualButton(VirtualButton button)
    {
        activeInput.RegisterVirtualButton(button);
    }

    public static void UnRegisterVirtualAxis(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException("name");
        }
        activeInput.UnregisterVirtualAxis(name);
    }

    public static void UnRegisterVirtualButton(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException("name");
        }
        activeInput.UnregisterVirtualButton(name);
    }


    public static bool AxisExists(string name)
    {
        return activeInput.AxisExists(name);
    }

    public static bool ButtonExists(string name)
    {
        return activeInput.ButtonExists(name);
    }


    // -- Axis handling --
    public static void SetAxis(string name, float value)
    {
        activeInput.SetAxis(name, value);
    }

    public static void SetAxisPositive(string name)
    {
        activeInput.SetAxisPositive(name);
    }

    public static void SetAxisNegative(string name)
    {
        activeInput.SetAxisNegative(name);
    }

    public static void SetAxisZero(string name)
    {
        activeInput.SetAxisZero(name);
    }

    // returns the platform appropriate axis for the given name
    public static float GetAxis(string name)
    {
        return GetAxis(name, false);
    }

    public static float GetAxisRaw(string name)
    {
        return GetAxis(name, true);
    }

    // private function handles both types of axis (raw and not raw)
    private static float GetAxis(string name, bool raw)
    {
        return activeInput.GetAxis(name, raw);
    }


    // -- Button handling --
    public static void SetButtonDown(string name)
    {
        activeInput.SetButtonDown(name);
    }

    public static void SetButtonUp(string name)
    {
        activeInput.SetButtonUp(name);
    }

    public static bool GetButton(string name)
    {
        return activeInput.GetButton(name);
    }

    public static bool GetButtonDown(string name)
    {
        return activeInput.GetButtonDown(name);
    }

    public static bool GetButtonUp(string name)
    {
        return activeInput.GetButtonUp(name);
    }



    public class VirtualAxis
    {
        private string name;
        private float value;

        public string Name
        {
            get { return name; }
        }

        public VirtualAxis(string name)
        {
            this.name = name;
        }

        public void Update(float value)
        {
            this.value = value;
        }

        public float GetValue()
        {
            return value;
        }

        public float GetValueRaw()
        {
            return value;
        }

        public void Remove()
        {
            UnRegisterVirtualAxis(name);
        }
    }


    public class VirtualButton
    {
        private string name;
        private bool pressed;

        private int lastPressedFrame = -5;
        private int releasedFrame = -5;

        public string Name
        {
            get { return name; }
        }

        public VirtualButton(string name)
        {
            this.name = name;
        }

        public void Press()
        {
            if (pressed)
            {
                return;
            }
            pressed = true;
            lastPressedFrame = Time.frameCount;
        }

        public void Release()
        {
            pressed = false;            
            releasedFrame = Time.frameCount;
        }

        public bool GetButton()
        {    
            return pressed;            
        }

        public bool GetButtonDown()
        {
            return lastPressedFrame - Time.frameCount == -1;
        }

        public bool GetButtonUp()
        {
            return (releasedFrame == Time.frameCount - 1);
        }

        public void Remove()
        {
            UnRegisterVirtualButton(name);
        }

    }
}
