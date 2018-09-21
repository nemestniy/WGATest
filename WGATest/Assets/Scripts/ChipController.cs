using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipController : MonoBehaviour {

    const string _leftMouseButton = "Fire1";

    public delegate void MethoodsContainer();
    public event MethoodsContainer OnButtonDown;
    public event MethoodsContainer OnButtonUp;
    public event MethoodsContainer OnButton;
    public event MethoodsContainer EnterCollider;
    public event MethoodsContainer ExitCollider;

    private float _mouseX;
    private float _mouseY;

    private void Update()
    {
        _mouseX = Input.mousePosition.x;
        _mouseY = Input.mousePosition.y;

        if (Input.GetButtonDown(_leftMouseButton))
        {
            OnButtonDown();
        }

        if (Input.GetButtonUp(_leftMouseButton))
        {
            OnButtonUp();
        }

        if (Input.GetButton(_leftMouseButton))
        {
            OnButton();
        }
    }

    private void OnMouseEnter()
    {
        EnterCollider();
    }

    private void OnMouseExit()
    {
        ExitCollider();
    }

    public float GetMouseX()
    {
        return _mouseX;
    }

    public float GetMouseY()
    {
        return _mouseY;
    }
}
