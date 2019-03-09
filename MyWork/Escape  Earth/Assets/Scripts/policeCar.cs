using UnityEngine;
using System.Collections;

public class policeCar : MonoBehaviour {

    private bool havePlaySires=false;
    private  AudioSource siresSound;
	
    void Awake()
    {
        siresSound = this.GetComponent<AudioSource>();
    }
	void Update () {
        if (havePlaySires == false && GameController.gamestate == GameController.Gamestate.End)
        {
            siresSound.Play();
            havePlaySires = true;
        }
	}
}
