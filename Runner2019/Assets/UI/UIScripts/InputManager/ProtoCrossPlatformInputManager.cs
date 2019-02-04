using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ProtoCrossPlatformInputManager 
{
    public enum ActiveInputMethod
    {
        Hardware,
        Touch
    }

    private static VirtualInput activeInput;

    private static VirtualInput touchInput;

    private static VirtualInput hardwareInput;




    static ProtoCrossPlatformInputManager()
    {
        touchInput = new MobileInput();
        hardwareInput = new StandaloneInput();
#if MOBILE_INPUT
        activeInput = touchInput;
#else
        activeInput = hardwareInput;
#endif
    }


    public static void SwitchActiveInputMethod(ActiveInputMethod activeInputMethod)
    {
        switch (activeInputMethod)
        {
            case ActiveInputMethod.Hardware:
                activeInput = hardwareInput;
                break;

            case ActiveInputMethod.Touch:
                activeInput = touchInput;
                break;
        }
    }


    public static bool AxisExists(string name)
    {
        return activeInput.AxisExists(name);
    }

    public static bool ButtonExists(string name)
    {
        return activeInput.ButtonExists(name);
    }

    // returns a reference to a named virtual axis if it exists otherwise null
    public static VirtualAxis VirtualAxisReference(string name)
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
        activeInput.UnRegisterVirtualAxis(name);
    }

    public static void UnRegisterVirtualButton(string name)
    {
        activeInput.UnRegisterVirtualButton(name);
    }
       


    public static void SetVirtualMousePositionX(float f)
    {
        activeInput.SetVirtualMousePositionX(f);
    }

    public static void SetVirtualMousePositionY(float f)
    {
        activeInput.SetVirtualMousePositionY(f);
    }

    public static void SetVirtualMousePositionZ(float f)
    {
        activeInput.SetVirtualMousePositionZ(f);
    }

    public static Vector3 mousePosition
    {
        get { return activeInput.MousePosition(); }
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
       



    // a controller gameobject (eg. a virtual GUI button) should call the
    // 'pressed' function of this class. Other objects can then read the
    // Get/Down/Up state of this button.
    public class VirtualButton
    {
        public string name { get; private set; }
        private bool pressed;
        public bool matchWithInputManager { get; private set; }

        private int lastPressedFrame = -5;
        private int releasedFrame = -5;
        



        public VirtualButton(string name)
            : this(name, true)
        {
        }

        public VirtualButton(string name, bool matchToInputSettings)
        {
            this.name = name;
            matchWithInputManager = matchToInputSettings;
        }



        // the controller gameobject should call Remove when the button is destroyed or disabled
        public void Remove()
        {
            UnRegisterVirtualButton(name);
        }



        // A controller gameobject should call this function when the button is pressed down
        public void Pressed()
        {
            if (pressed)
            {
                return;
            }
            pressed = true;
            lastPressedFrame = Time.frameCount;
        }

        // A controller gameobject should call this function when the button is released
        public void Released()
        {
            pressed = false;
            releasedFrame = Time.frameCount;
        }



        // these are the states of the button which can be read via the cross platform input system
        public bool GetButton
        {
            get { return pressed; }
        }

        public bool GetButtonDown
        {
            get
            {
                return lastPressedFrame - Time.frameCount == -1;
            }
        }

        public bool GetButtonUp
        {
            get
            {
                return (releasedFrame == Time.frameCount - 1);
            }
        }    
    }


    // virtual axis and button classes - applies to mobile input
    // Can be mapped to touch joysticks, tilt, gyro, etc, depending on desired implementation.
    // Could also be implemented by other input devices - kinect, electronic sensors, etc
    public class VirtualAxis
    {
        public string name { get; private set; }
        private float value;
        public bool matchWithInputManager { get; private set; }




        public VirtualAxis(string name)
            : this(name, true)
        {
        }

        public VirtualAxis(string name, bool matchToInputSettings)
        {
            this.name = name;
            matchWithInputManager = matchToInputSettings;
        }



        // removes an axes from the cross platform input system
        public void Remove()
        {
            UnRegisterVirtualAxis(name);
        }



        // a controller gameobject (eg. a virtual thumbstick) should update this class
        public void Update(float value)
        {
            this.value = value;
        }



        public float GetValue
        {
            get { return value; }
        }

        public float GetValueRaw
        {
            get { return value; }
        }
    }

}
