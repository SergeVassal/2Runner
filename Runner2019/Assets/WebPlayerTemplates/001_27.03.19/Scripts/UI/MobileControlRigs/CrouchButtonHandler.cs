using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrouchButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    CrossPlatformInputManager.VirtualButton crouchButton;
    


    private void OnEnable()
    {
        CreateVirtualButton();
    }


    private void CreateVirtualButton()
    {
        crouchButton = new CrossPlatformInputManager.VirtualButton("Crouch");
        CrossPlatformInputManager.RegisterVirtualButton(crouchButton);
    }


    public void OnPointerDown(PointerEventData eventData)
    {        
        crouchButton.Press();
    }


    public void OnPointerUp(PointerEventData eventData)
    {        
        crouchButton.Release();
    }
}
