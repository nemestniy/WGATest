using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : ChipController {

    private bool _approveMoving;
    private bool _onFreePlace;
    public bool _onBlock;

    private Vector3 _posFreePlace;
    private Vector3 _lastPosition;
    private SpriteRenderer _spriteRenderer;

    private void OnValidate()
    {
        if(transform.tag == TagManager.Chip)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = new Vector4(0, 0.5f, 0, 1);
            OnButton += Chip_OnButton;
            OnButtonDown += Chip_OnButtonDown;
            OnButtonUp += Chip_OnButtonUp;
            EnterCollider += Chip_EnterCollider;
            ExitCollider += Chip_ExitCollider;
        }
    }

    private void Start()
    {
        _onBlock = false;
    }

    private void Chip_ExitCollider()
    {
        _approveMoving = false;
        _spriteRenderer.color = new Vector4(0, 0.5f, 0, 1);
    }

    private void Chip_EnterCollider()
    {
        _approveMoving = true;
        _spriteRenderer.color = new Vector4(0, 0.75f, 0, 1);
    }

    private void Chip_OnButtonUp()
    {
        if(_approveMoving)
            _spriteRenderer.color = new Vector4(0, 0.75f, 0, 1);
        if (_onFreePlace)
            transform.position = _posFreePlace;
        if (_onBlock)
        {
            transform.position = _lastPosition;
            _onBlock = false;
        }
    }

    private void Chip_OnButtonDown()
    {
        _onFreePlace = false;
        _lastPosition = transform.position;
    }

    private void Chip_OnButton()
    {
        if (_approveMoving)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3 (pos.x, pos.y, 0);
            _spriteRenderer.color = new Vector4(0, 1, 0, 1);
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == TagManager.FreePlace)
        {
            Vector3 pos = collision.transform.position;
            _posFreePlace = new Vector3(pos.x, pos.y, 0);
            _onFreePlace = true;
        }

        if (collision.tag == TagManager.Block)
            _onBlock = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TagManager.FreePlace)
            _onFreePlace = false;
        if (collision.tag == TagManager.Block)
            _onBlock = false;
    }
}
