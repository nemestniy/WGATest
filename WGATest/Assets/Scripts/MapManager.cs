using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    [SerializeField]
    private GameObject Block;

    [SerializeField]
    private int NumberOfBlocks;

    private GameObject[] _freePlaces;
    private List<GameObject> _chips;

    public delegate void MethoodsContainer();
    public event MethoodsContainer BlocksGenerated;                                 //Событие, происходящее после генерации блоков

    private void Awake()
    {
        _freePlaces = GameObject.FindGameObjectsWithTag(TagManager.FreePlace);
        _chips = ArrChipsToList(GameObject.FindGameObjectsWithTag(TagManager.Chip));
    }

    private void Start()
    {
        //GenerateBlocks();                                                         
    }

    private List<GameObject> ArrChipsToList(GameObject[] arrChips)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (GameObject chip in arrChips)
            list.Add(chip);
        return list;
    }

    public GameObject[] GetMapFreePlaces()
    {   
        return _freePlaces;                                                         //Возвращает массив всех свободных мест
    }

    public List<GameObject> GetChipsMap()
    {
        return _chips;                                                              //Возвращает массив фишек на карте
    }

    public void GenerateBlocks()
    {
        for (int i = 0; i < NumberOfBlocks; i++)
            GenerateBlock();                                                        //Случайная генерация блоков
        BlocksGenerated();
    }

    private void GenerateBlock()
    {
        int i = Random.Range(0, _freePlaces.Length);
        if (_freePlaces[i].tag == TagManager.FreePlace)                             //Проверка:свободное ли место для генерации
        {
            Vector3 pos = _freePlaces[i].transform.position;
            _freePlaces[i] = Block;
            Instantiate(Block, pos, Quaternion.identity);
        }
        else
        {
            GenerateBlock();
        }
    }

    public void SetChipsMap(List<GameObject> chips)
    {
        _chips = chips;
    }

    public void ClearChipsMap()
    {
        foreach(GameObject chip in _chips)
        {
            chip.SetActive(false);
            Destroy(chip);
        }
        _chips = null;
    }
}