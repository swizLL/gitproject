using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Square : MonoBehaviour {
    public Text Number;
    public int number
    {
        set
        {
            Number.text = value.ToString();
        }
    }
    public void hide()
    {
        this.gameObject.SetActive(false);
    }
    public void show()
    {
        this.gameObject.SetActive(true);
    }	
}
