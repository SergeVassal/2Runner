using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonHandler : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    ProtoCrossPlatformInputManager.VirtualButton jumpButton;




    private void OnEnable()
    {
        CreateVirtualButton();
    }


    private void CreateVirtualButton()
    {
        jumpButton = new ProtoCrossPlatformInputManager.VirtualButton("JumpButton");
        ProtoCrossPlatformInputManager.RegisterVirtualButton(jumpButton);
    }    


    public void OnPointerDown(PointerEventData eventData)
    {
        jumpButton.Pressed();        
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        jumpButton.Released();        
    }
}
