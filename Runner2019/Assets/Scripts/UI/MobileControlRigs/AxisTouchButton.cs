using UnityEngine;
using UnityEngine.EventSystems;

public class AxisTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string axisName="Horizontal";
    [SerializeField] private float axisMaxValue;
    [SerializeField] private float axisResponseSpeed;
    [SerializeField] private float axisGravity;

    private bool isPressed;
    private AxisTouchButton pairedWithButton;
    private CrossPlatformInputManager.VirtualAxis virtualAxis;



    private void Start()
    {
        FindPairedButton();
        RegisterVirtualAxis();
    }

    private void FindPairedButton()
    {
        AxisTouchButton[] axisTouchButtons = FindObjectsOfType<AxisTouchButton>() as AxisTouchButton[];        

        if (axisTouchButtons != null)
        {
            for (int i = 0; i < axisTouchButtons.Length; i++)
            {
                if (axisTouchButtons[i].axisName == axisName && axisTouchButtons[i] != this)
                {
                    pairedWithButton = axisTouchButtons[i];
                }
            }
        }
        
    }

    private void RegisterVirtualAxis()
    {
        if (!CrossPlatformInputManager.AxisExists(axisName))
        {
            virtualAxis = new CrossPlatformInputManager.VirtualAxis(axisName);
            CrossPlatformInputManager.RegisterVirtualAxis(virtualAxis);
        }
        else
        {
            virtualAxis = CrossPlatformInputManager.VirtualAxisReference(axisName);
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

    private void FixedUpdate()
    {
        if (isPressed)
        {
            virtualAxis.Update(Mathf.MoveTowards(virtualAxis.GetValue(), axisMaxValue, axisResponseSpeed * Time.fixedDeltaTime));           
        }
        else if(!isPressed && !pairedWithButton.IsPressed())
        {
            virtualAxis.Update(Mathf.MoveTowards(virtualAxis.GetValue(), 0f, axisGravity * Time.fixedDeltaTime));            
        }
    }

    public bool IsPressed()
    {
        return isPressed;
    }
}