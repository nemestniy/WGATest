using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlace : ChipController {

    private void OnValidate()
    {
        OnButton += FreePlace_OnButton;
        OnButtonDown += FreePlace_OnButtonDown;
        OnButtonUp += FreePlace_OnButtonUp;
        EnterCollider += FreePlace_EnterCollider;
        ExitCollider += FreePlace_ExitCollider;
    }

    private void FreePlace_ExitCollider()
    {
        
    }

    private void FreePlace_EnterCollider()
    {
        
    }

    private void FreePlace_OnButtonUp()
    {
        
    }

    private void FreePlace_OnButtonDown()
    {
        
    }

    private void FreePlace_OnButton()
    {
        
    }


}
