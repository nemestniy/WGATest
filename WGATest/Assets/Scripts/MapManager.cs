using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _freePlaces;

    private void OnValidate()
    {
        _freePlaces = GameObject.FindGameObjectsWithTag(TagManager.FreePlace);              

    }

    public GameObject[] GetMap()
    {   
        return _freePlaces;                                                         //Возвращает массив всех свободных мест
    }
}