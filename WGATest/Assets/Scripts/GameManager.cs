using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private MapManager _map;

    [SerializeField]
    private int _numberOfChips;

    [SerializeField]
    private GameObject _greenChip;                                     

    [SerializeField]
    private GameObject _blueChip;                                      

    [SerializeField]
    private GameObject _yellowChip;                                    

    [SerializeField]
    private Text _text;

    [SerializeField]
    private GameObject _startButton;

    private Color _greenColor = new Color(0, 0.5f, 0, 1);                       
    private Color _blueColor = new Color(0, 0, 0.5f, 1);                            //Цвета фишек, по которым производится поиск
    private Color _yellowColor = new Color(0.5f, 0.5f, 0, 1);

    private List<GameObject> _gChips;
    private List<GameObject> _bChips;                                               //Списки фишек упорядоченные по цвету
    private List<GameObject> _yChips;

    private bool _isGreenRow;
    private bool _isBlueRow;                                                        //Возвращает true, если синий собран в ряд
    private bool _isYellowRow;

    private void Awake()
    {
        _map = GameObject.Find("Map").GetComponent<MapManager>();                   //Получение объекта map

        _text.enabled = false;
    }

    public void OnClickStart()
    {
        List<GameObject> chips = _map.GetChipsMap();

        if(chips != null)
            _map.ClearChipsMap();

        for (int i = 0; i < _numberOfChips; i++)
        {
            var gChip = Instantiate(_greenChip) as GameObject;
            Vector3 gVector = GetStartPosition();                                       //Получаем позицию на карте
            gChip.transform.position = gVector;                                         //Передаем позицию зеленой фишке
            gChip.GetComponent<Chip>().SetLastPosition(gVector);                        //Передаем позицию, как ее последнее место 
            chips.Add(gChip);                                                           //Заносим фишку в список фишек
            _map.SetChipsMap(chips);                                                    //Редактируем глобальный список карты

            var bChip = Instantiate(_blueChip) as GameObject;
            Vector3 bVector = GetStartPosition();
            bChip.transform.position = bVector;
            bChip.GetComponent<Chip>().SetLastPosition(bVector);
            chips.Add(bChip);
            _map.SetChipsMap(chips);

            var yChip = Instantiate(_yellowChip) as GameObject;
            Vector3 yVector = GetStartPosition();
            yChip.transform.position = yVector;
            yChip.GetComponent<Chip>().SetLastPosition(yVector);
            chips.Add(yChip);
            _map.SetChipsMap(chips);
        }

        _startButton.SetActive(false);
        _text.enabled = false;

        _gChips = GetChipFromColor(_greenColor);                                                                              
        _bChips = GetChipFromColor(_blueColor);                                   //Получение упорядоченного списка по цветам
        _yChips = GetChipFromColor(_yellowColor);
    }

    public Vector3 GetStartPosition()
    {
        List<GameObject> chips = _map.GetChipsMap();
        GameObject[] freePlaces = _map.GetMapFreePlaces();                                                                     //Получение списка объектов
        int i = Random.Range(0, freePlaces.Length);
        Vector3 startPos = freePlaces[i].transform.position;
        if (chips != null)
        {
            foreach (GameObject chip in chips)
                if ((Vector2)chip.transform.position == (Vector2)startPos || freePlaces[i].tag != TagManager.FreePlace)         //Проверка позиции на то, есть ли там другая фишка
                    return GetStartPosition();
        }
        return new Vector3(startPos.x, startPos.y, 0);
    }

    private void Update()
    {
        _isGreenRow = CheckRowChips(_gChips);
        _isBlueRow = CheckRowChips(_bChips);                                                //Проверка на расположение фишек
        _isYellowRow = CheckRowChips(_yChips);

        if (_isGreenRow && _isBlueRow && _isYellowRow)                                      //Если все расположены в ряд, то игра фиксирует победу
        {
            _text.enabled = true;
        }
    }

    private bool CheckRowChips(List<GameObject> chips)
    {
        if (chips != null)
        {
            bool resultX = true;
            bool resultY = true;
            Vector2 x = new Vector2(chips[1].transform.localScale.x * 5, chips[1].transform.localScale.y / 20);
            Vector2 y = new Vector2(chips[1].transform.localScale.x / 20, chips[1].transform.localScale.y * 5);
            List<Color> rowChipsX = ArrToList(Physics2D.OverlapBoxAll(chips[1].transform.position, x, 0));            //Список объектов выстроенных в линию по оси Х
            List<Color> rowChipsY = ArrToList(Physics2D.OverlapBoxAll(chips[1].transform.position, y, 0));            //Список объектов выстроенных в линию по оси У

            foreach(GameObject chip in chips)
            {
                if (!rowChipsX.Contains(chip.GetComponent<SpriteRenderer>().color))
                {
                    resultX = false;                                                                            //Проверка, есть ли фишки одного цвета в линии
                }
                else
                {
                    rowChipsX.Remove(chip.GetComponent<SpriteRenderer>().color);
                }


                if (!rowChipsY.Contains(chip.GetComponent<SpriteRenderer>().color))
                {
                    resultY = false;
                }
                else
                {
                    rowChipsY.Remove(chip.GetComponent<SpriteRenderer>().color);
                }
            }
            return (resultX || resultY);                                                                    
        }
        else
            return false;
    }

    private List<Color> ArrToList(Collider2D[] transforms)
    {
            List<Color> list = new List<Color>();
            foreach (Collider2D transfor in transforms)
                list.Add(transfor.transform.GetComponent<SpriteRenderer>().color);                       //Перевод массива коллайдеров в лист цветов
            return list;
    }

    private List<GameObject> GetChipFromColor(Color color)
    {
        List<GameObject> chips = new List<GameObject>();
        foreach(GameObject chip in _map.GetChipsMap())
        {
            if (chip.GetComponent<SpriteRenderer>().color == color)                                      //Получение списка объектов по цвету
                chips.Add(chip);
        }
        return chips;
    }
}
