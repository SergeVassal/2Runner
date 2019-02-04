using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AxisTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string axisName = "Horizontal"; // The name of the axis
    [SerializeField] private float axisValue = -1; // The axis that the value has
    [SerializeField] private float responseSpeed = 3; // The speed at which the axis touch button responds
    [SerializeField] private float returnToCentreSpeed = 3; // The speed at which the button will return to its centre

    AxisTouchButton pairedWithButton; // Which button this one is paired with
    private ProtoCrossPlatformInputManager.VirtualAxis horizontalAxis;
    private bool isPressed = false;




    private void OnEnable()
    {
        CreateVirtualAxis();
        FindPairedButton();
    }


    void OnDisable()
    {
        // The object is disabled so remove it from the cross platform input system
        horizontalAxis.Remove();
    }


    private void CreateVirtualAxis()
    {
        if (!ProtoCrossPlatformInputManager.AxisExists(axisName))
        {
            // if the axis doesnt exist create a new one in cross platform input
            horizontalAxis = new ProtoCrossPlatformInputManager.VirtualAxis(axisName);
            ProtoCrossPlatformInputManager.RegisterVirtualAxis(horizontalAxis);
        }
        else
        {
            horizontalAxis = ProtoCrossPlatformInputManager.VirtualAxisReference(axisName);
        }
    }


    void FindPairedButton()
    {
        // find the other button witch which this button should be paired
        // (it should have the same axisName)
        var otherAxisButtons = FindObjectsOfType(typeof(AxisTouchButton)) as AxisTouchButton[];

        if (otherAxisButtons != null)
        {
            for (int i = 0; i < otherAxisButtons.Length; i++)
            {
                if (otherAxisButtons[i].axisName == axisName && otherAxisButtons[i] != this)
                {
                    pairedWithButton = otherAxisButtons[i];
                }
            }
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;        
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }


    public bool IsPressed()
    {
        return isPressed;
    }


    private void FixedUpdate()
    {
        if (isPressed)
        {
            horizontalAxis.Update(Mathf.MoveTowards(horizontalAxis.GetValue, axisValue, responseSpeed * Time.deltaTime));            
        }
        else if (!isPressed && !pairedWithButton.IsPressed())
        {
            horizontalAxis.Update(Mathf.MoveTowards(horizontalAxis.GetValue, 0, returnToCentreSpeed * Time.deltaTime));            
        }        
    }


}
