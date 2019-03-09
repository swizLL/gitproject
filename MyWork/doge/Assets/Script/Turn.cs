using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum turnDirect
{
    Left,
    Right
}
public class Turn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public turnDirect direct;
    private Transform player;
    public dogemove doge;
    private bool isPress=false;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (isPress&&direct==turnDirect.Right)
        {
            player.Rotate(Vector3.up * Time.deltaTime * doge.rotspeed);
        }else if(isPress && direct == turnDirect.Left)
        {
            player.Rotate(-Vector3.up * Time.deltaTime * doge.rotspeed);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
    }
}

