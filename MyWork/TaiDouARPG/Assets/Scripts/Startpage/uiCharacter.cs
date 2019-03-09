using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class uiCharacter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform rotateCharacter;
  
    private bool canRotate;
    //private bool isMan=true;
    public GameObject changeMan;
    public RawImage changeGirl;
    // Update is called once per frame
    void Update ()
    {
           if(canRotate)
        {
            float rot = Input.GetAxis("Mouse X");
            rotateCharacter.Rotate(0, -rot, 0);
        }
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        canRotate = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        canRotate = false;
    }
    public void selectMan()
    {
        changeMan.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        changeGirl.transform.localScale = new Vector3(1, 1, 1);
        UIController._instance.charaIndex = 0;
    }
    public void selectGirl()
    {
        changeGirl.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        changeMan.transform.localScale = new Vector3(1, 1, 1);
        UIController._instance.charaIndex = 1;
    }
}
