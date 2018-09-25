using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : ChipController {

    [SerializeField]
    private MapManager _map;

    [SerializeField]
    private Color _deactive;                                         //Цвета фишки в разных случаях

    [SerializeField]
    private Color _underCursor;

    [SerializeField]
    private Color _active;                                   

    private bool _approveMoving;                                    //Возвращает true, если курсор над фишкой
    private bool _onFreePlace;                                      //Возвращает true, если фишка над свободным местом
    private bool _onBlock;                                          //Возвращает true, если фишка над запрещенным местом
    private bool _onChip;                                           //Возвращает true, если фишка находится над другой фишкой
    private bool _isActive;                                         //Возвращает true, если фишка активна
    private bool _keyPressed;                                       //Возвращает true, если клавиша мыши нажата

    private Vector3 _nextPos;                                       //Следующая позиция фишки после отпускания левой клавиши мыши или нажатия клавиши клавиатуры
    private Vector3 _lastPosition;                                  //Последняя похиция перед перемещением фишки
    private SpriteRenderer _spriteRenderer;                         
    private FreePlace _freePlace;                                   //Текущее место под фишкой
    private List<GameObject> _chips;                                    //Список фишек на карте

    private void Awake()
    {
        if (transform.tag == TagManager.Chip)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = _deactive;                                                                       //Обозначение объекта в соответствии с его тэгом 
            OnButton += Chip_OnButton;
            OnButtonDown += Chip_OnButtonDown;
            OnButtonUp += Chip_OnButtonUp;
            EnterCollider += Chip_EnterCollider;                                                                    //Инициализация всех событий
            ExitCollider += Chip_ExitCollider;
            _onFreePlace = true;
            _map = GameObject.Find("Map").GetComponent<MapManager>();                                                //Определение карты
            _map.BlocksGenerated += Map_BlocksGenerated;                                                             //Событие, когда сгенерированны все блоки
        }
    }

    private void Map_BlocksGenerated()
    {
                                                                                                                    //Пока пусто из за неопределенности 
    }

    private void Chip_ExitCollider()
    {
        if (!_keyPressed)
            _approveMoving = false;
        if(!IsActive())
            _spriteRenderer.color = _deactive;
    }

    private void Chip_EnterCollider()
    {
        if (!_keyPressed)
        {
            _approveMoving = true;
            if (!IsActive())
                _spriteRenderer.color = _underCursor;
        }
    }

    private void Chip_OnButtonUp()
    {
        _keyPressed = false;
        if (_onFreePlace)
            transform.position = _nextPos;
        if (!_onFreePlace)
        {
            transform.position = _lastPosition;
            if (_onBlock)
                _onBlock = false;
        }
        if(_freePlace != null)
            _freePlace = null;
    }

    private void Chip_OnButtonDown()
    {
        _keyPressed = true;
        _onFreePlace = false;
        _onBlock = false;
        _lastPosition = transform.position;
        if (_approveMoving)
        {
            SetActive();
        }
    }

    private void Chip_OnButton()
    {
        if (_approveMoving)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3 (pos.x, pos.y, 0);                         //Перемещение фишки мышью
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagManager.FreePlace)
        {
            Vector3 pos;
            if (!_onBlock)
            {
                if (_freePlace != null)
                {
                    if (!_freePlace.GetNeighbors().Contains(collision.transform))                               //Контроль перемещения фишки на соседнее место
                        pos = _lastPosition;
                    else
                        pos = collision.transform.position;

                }
                else
                {
                    pos = _lastPosition;
                }
            }
            else
            {
                pos = _lastPosition;
            }
            _nextPos = new Vector3(pos.x, pos.y, 0);
        }

        if (collision.tag == TagManager.Block || collision.tag == TagManager.Chip)
        {
            _onBlock = true;
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

        if (collision.tag == TagManager.Block || collision.tag == TagManager.Chip)
        {
            _onBlock = true;                                                            //Проверка, над каким объектом находится фишка в данный момент
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TagManager.FreePlace)
        {
            _onFreePlace = false;

        }
        if (collision.tag == TagManager.Block || collision.tag == TagManager.Chip)
            _onBlock = false;
    }

    public bool IsActive()
    {
        return _isActive;                                                                    //Возвращает true, если фишка активна
    }

    public void Deactivate()
    {
        if(_isActive)
            _isActive = false;                                                              //Деактивирует текущую фишку
        _spriteRenderer.color = _deactive;
    }

    private void SetActive()
    {
        if (_chips != null && !_keyPressed)
        {
            foreach (GameObject chip in _chips)
                chip.GetComponent<Chip>().Deactivate();                                     //Метод, делающий текущую фишку активной
            _isActive = true;
            _spriteRenderer.color = _active;
        }
    }

    public void SetLastPosition(Vector3 position)
    {
        _lastPosition = position;                                                           //Смена последней позиции
    }
}
