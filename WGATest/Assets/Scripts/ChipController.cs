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
    public event MethoodsContainer OnLeft;
    public event MethoodsContainer OnRight;
    public event MethoodsContainer OnUp;
    public event MethoodsContainer OnDown;

    private float _mouseX;
    private float _mouseY;

    private void Update()
    {
        _mouseX = Input.mousePosition.x;
        _mouseY = Input.mousePosition.y;

        if (Input.GetButtonDown(_leftMouseButton))
        {
            OnButtonDown();                                         //Нажатие на левую клавишу мыши
        }

        if (Input.GetButtonUp(_leftMouseButton))
        {
            OnButtonUp();                                           //Левая клавиша мыши снята с зажатия
        }

        if (Input.GetButton(_leftMouseButton))
        {
            OnButton();                                             //Левая клавиша мыши зажата
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeft();                                               //Нажатие на клавишу "Влево"
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRight();                                              //Нажатие на клавишу "Вправо"
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnUp();                                                 //Нажатие на клавишу "Вверх"
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnDown();                                               //Нажатие на клавишу "Вниз"
        }
    }

    private void OnMouseEnter()
    {
        EnterCollider();                                            //Курсор внутри области коллайдера
    }

    private void OnMouseExit()
    {
        ExitCollider();                                             //Курсор вышел из области коллайдера
    }

    public float GetMouseX()
    {
        return _mouseX;                                             //Абцисса курсора 
    }

    public float GetMouseY()
    {
        return _mouseY;                                             //Ордината курсора
    }
}
