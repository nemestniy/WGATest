using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlace : ChipController {

    private SpriteRenderer _spriteRenderer;

    private Collider2D[] PlacesX;
    private Collider2D[] PlacesY;

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = new Vector4(0.5f, 0.5f, 0.5f, 1);
        OnButton += FreePlace_OnButton;
        OnButtonDown += FreePlace_OnButtonDown;
        OnButtonUp += FreePlace_OnButtonUp;
        EnterCollider += FreePlace_EnterCollider;
        ExitCollider += FreePlace_ExitCollider;
    }

    private void Start()
    {
        Vector2 x = new Vector2(transform.position.y / 2, transform.localScale.x * 2);
        PlacesX = Physics2D.OverlapBoxAll(transform.position, x, 0);                            //Получение соседей по оси Х
        Vector2 y = new Vector2(transform.localScale.y * 2, transform.localScale.y / 2);    
        PlacesY = Physics2D.OverlapBoxAll(transform.position, y, 0);                            //Получение соседей по оси У
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

    public List<Transform> GetNeighbors()
    {
        List<Transform> list = new List<Transform>();
        for (int i = 0; i < PlacesX.Length; i++)
            if(!list.Contains(PlacesX[i].transform))
                list.Add(PlacesX[i].transform);                                         
        for (int i = 0; i < PlacesY.Length; i++)                            //Метод, заносящий всех соседей в один список
            if (!list.Contains(PlacesY[i].transform))
                list.Add(PlacesY[i].transform);
        return list;
    }
}
