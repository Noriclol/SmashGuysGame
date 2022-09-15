using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public void Awake()
    {
        Main.Instance.GameManager.level = this;
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            Main.Instance.GameManager.UnusedSpawnPoints.Add(i);
        }
        Main.Instance.GameManager.PlayerSetup(Main.Instance.GameManager.players);
    }
    
}
