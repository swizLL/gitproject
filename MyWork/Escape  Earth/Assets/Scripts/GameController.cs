using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public enum Gamestate
    {
        Menu,
        Playing,
        End
    }
    public static Gamestate gamestate = Gamestate.Menu;
    public GameObject tabtoStartUI;
    public GameObject gameoverUI;
    void Update()
    {
        if (gamestate == Gamestate.Menu)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gamestate = Gamestate.Playing;
                tabtoStartUI.SetActive(false);
            }
        }
        if (gamestate == Gamestate.End)
        {
            gameoverUI.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                gamestate = Gamestate.Menu;
                Application.LoadLevel(0);
            }
        }

    }

}
