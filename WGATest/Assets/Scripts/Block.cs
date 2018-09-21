using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : ChipController {

    private SpriteRenderer _spriteRenderer;

    private void OnValidate()
    {
        if (transform.tag == TagManager.Block)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = new Vector4(1, 0, 0, 1);
            OnButton += Block_OnButton;
            OnButtonDown += Block_OnButtonDown;
            OnButtonUp += Block_OnButtonUp;
            EnterCollider += Block_EnterCollider;
            ExitCollider += Block_ExitCollider;
        }
    }

    private void Block_ExitCollider()
    {
        
    }

    private void Block_EnterCollider()
    {
        
    }

    private void Block_OnButtonUp()
    {
        
    }

    private void Block_OnButtonDown()
    {
        
    }

    private void Block_OnButton()
    {
        
    }
}
