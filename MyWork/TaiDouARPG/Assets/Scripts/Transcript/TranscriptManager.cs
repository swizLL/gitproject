﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptManager : MonoBehaviour {
    public static TranscriptManager _instance;

    public GameObject player;
    public List<GameObject> enemyList = new List<GameObject>();
    private void Awake()
    {
        _instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
