using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : ChipController {

    [SerializeField]
    private MapManager Map;

    private bool _approveMoving;                                    //Возвращает true, если курсор над фишкой
    private bool _onFreePlace;                                      //Возвращает true, если фишка над свободным местом
    private bool _onBlock;                                          //Возвращает true, если фишка над запрещенным местом

    private Vector3 _nextPos;                                       //Следующая позиция фишки после отпускания левой клавиши мыши или нажатия клавиши клавиатуры
    private Vector3 _lastPosition;                                  //Последняя похиция перед перемещением фишки
    private SpriteRenderer _spriteRenderer;                         
    public FreePlace _freePlace;                                   

    private void OnValidate()
    {
        if(transform.tag == TagManager.Chip)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = new Vector4(0, 0.5f, 0, 1);     //Обозначение объекта в соответствии с его тэгом 
            OnButton += Chip_OnButton;
            OnButtonDown += Chip_OnButtonDown;
            OnButtonUp += Chip_OnButtonUp;
            EnterCollider += Chip_EnterCollider;                    //Инициализация всех событий
            ExitCollider += Chip_ExitCollider;
            OnLeft += Chip_OnLeft;
            OnRight += Chip_OnRight;
            OnUp += Chip_OnUp;
            OnDown += Chip_OnDown;
            _onFreePlace = true;
            Map = GameObject.Find("Map").GetComponent<MapManager>();//Определение карты
        }
    }

    private void Start()
    {
        GameObject[] freePlaces = Map.GetMap();                                                         //Получение списка объектов
        Vector3 startPos = freePlaces[Random.Range(0, freePlaces.Length)].transform.position;
        transform.position = new Vector3(startPos.x, startPos.y, 0);                            //Инициализация объекта
        _lastPosition = transform.position;
    }

    private void Chip_OnDown()
    {
       
    }

    private void Chip_OnUp()
    {
        
    }

    private void Chip_OnRight()
    {
       
    }

    private void Chip_OnLeft()
    {
        
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
            transform.position = _nextPos;
        if (_onBlock)
        {
            transform.position = _lastPosition;
            _onBlock = false;
        }
        if(_freePlace != null)
            _freePlace = null;
    }

    private void Chip_OnButtonDown()
    {
        _onFreePlace = false;
        _onBlock = false;
        _lastPosition = transform.position;
    }

    private void Chip_OnButton()
    {
        if (_approveMoving)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3 (pos.x, pos.y, 0);                         //Перемещение фишки мышью
            _spriteRenderer.color = new Vector4(0, 1, 0, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.position);
        if (collision.tag == TagManager.FreePlace)
        {
            Vector3 pos;
            if (_freePlace != null)
            {
                if (!_freePlace.GetNeighbors().Contains(collision.transform))
                    pos = _lastPosition;                                              //Контроль перемещения фишки на соседнее место
                else
                    pos = collision.transform.position;

            }
            else
            {
                pos = _lastPosition;
            }
            _nextPos = new Vector3(pos.x, pos.y, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == TagManager.FreePlace)
        {
            _onFreePlace = true;
            if ((Vector2)_lastPosition == (Vector2)collision.transform.position)
            {
                _freePlace = collision.GetComponent<FreePlace>();                       //Получение данных свободного места перед перемещением
            }
        }

        if (collision.tag == TagManager.Block)
            _onBlock = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TagManager.FreePlace)
        {
            _onFreePlace = false;

        }
        if (collision.tag == TagManager.Block)
            _onBlock = false;
    }
}
