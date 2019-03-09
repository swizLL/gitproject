using UnityEngine;
using System.Collections;

public class turnToChossScene : MonoBehaviour {
    public  GameObject chooseObj;
	public void changeToChoose()
    {
        chooseObj.SetActive(true);
    }
}
