using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController _instance;
    public GameObject NameChangeBar;
    public GameObject PlayerStatus;
    private void Awake()
    {
        _instance = this;
    }
    public static int GetRequireExpByLevel(int level)
    {
        return ((100 + (100 + 10 * (level - 2))) * (level - 1)) / 2;
    }
}
