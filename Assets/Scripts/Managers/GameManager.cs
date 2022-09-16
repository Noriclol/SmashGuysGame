using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //references/prefabs;
    public LevelHandler level;
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;
    
    public List<int> UnusedSpawnPoints;
    public List<GameObject> SpawnedPlayers;
    public PlayerSelectCount players;

    public List<Color> PlayerColors;
    


    //Onstart
    public void PlayerSetup(PlayerSelectCount players)
    {
        switch (players)
        {
            case PlayerSelectCount.two:
                for (int i = 0; i < 2; i++)
                {
                   //InstantiatePlayer
                   SpawnedPlayers.Add(SpawnCharacter((PlayerChoice)i));
                }
                break;


            case PlayerSelectCount.three:
                for (int i = 0; i < 3; i++)
                {
                    SpawnedPlayers.Add(SpawnCharacter((PlayerChoice)i));
                    //InstantiatePlayer
                    //InstantiateCameraasChildofPlayer
                }
                break;


            case PlayerSelectCount.four:
                for (int i = 0; i < 4; i++)
                {
                    SpawnedPlayers.Add(SpawnCharacter((PlayerChoice)i));
                    //InstantiatePlayer
                    //InstantiateCameraasChildofPlayer
                }
                break;
        }
        
        
        //camera setup
        CamDim config = new CamDim();
        switch (players)
        {
            case PlayerSelectCount.two:
                config = new CamDim(new Vector4(0f, 0.5f, 1.0f, 0.5f), new Vector4(0f, 0f, 1f, 0.5f));
                break;
            case PlayerSelectCount.three:
                config = new CamDim(new Vector4(0f, 0.5f, 1.0f, 0.5f), new Vector4(0f, 0f, 0.5f, 0.5f), new Vector4(0.5f, 0f, 0.5f, 0.5f));

                break;
            case PlayerSelectCount.four:
                config = new CamDim(new Vector4(0f, 0.5f, 0.5f, 0.5f), new Vector4(0.5f, 0.5f, 0.5f, 0.5f), new Vector4(0f, 0f, 0.5f, 0.5f), new Vector4(0.5f, 0f, 0.5f, 0.5f));
                
                break;
        }
        
        for (int i = 0; i < SpawnedPlayers.Count; i++)
        {
            switch (i)
            {
                case 0:
                    var cam0 = CameraSetup(config.c1, SpawnedPlayers[i]);
                    cam0.SetBorderColor(PlayerColors[i]);
                    break;
                case 1:
                    var cam1 = CameraSetup(config.c2, SpawnedPlayers[i]);
                    cam1.SetBorderColor(PlayerColors[i]);
                    break;
                case 2:
                    var cam2 = CameraSetup(config.c3, SpawnedPlayers[i]);
                    cam2.SetBorderColor(PlayerColors[i]);
                    break;
                case 3:
                    var cam3 = CameraSetup(config.c4, SpawnedPlayers[i]);
                    cam3.SetBorderColor(PlayerColors[i]);
                    break;
                default:
                    return;
            }
        }
    }

    public GameObject SpawnCharacter(PlayerChoice player)
    {
        
        //get Spawnpoint
        int selectedpoint = Random.Range(0, UnusedSpawnPoints.Count);
        Vector3 pos = level.SpawnPoints[UnusedSpawnPoints[selectedpoint]].transform.position;
        UnusedSpawnPoints.Remove(UnusedSpawnPoints[selectedpoint]);

        //Generate PlayerControllerScript
        // PlayerController newPlayerController = new PlayerController();
        // newPlayerController.chosenInput = player;
        // newPlayerController.playerColor = PlayerColors[(int)player];
        
        //Instantiate Player
        var newPlayer = Instantiate(PlayerPrefab, pos, quaternion.identity);
        newPlayer.GetComponent<PlayerController>().SetPlayerInput(player, PlayerColors[(int)player]);
        return newPlayer;
    }

    public CameraHandler CameraSetup(Vector4 config, GameObject parent)
    {
        var newCam = Instantiate(CameraPrefab, parent.transform.position, quaternion.identity);
        
        newCam.transform.SetParent(parent.transform);
        newCam.transform.localPosition = Vector3.back;
        
        var CameraRaw = newCam.GetComponent<Camera>();
        CameraRaw.rect = new Rect(config.x, config.y, config.z, config.w);

        return newCam.GetComponent<CameraHandler>();
    }

    //OnDestroy
    public void LevelClear()
    {
        UnusedSpawnPoints.Clear();
        SpawnedPlayers.Clear();
        level = null;
    }
}

public struct CamDim
{
    public Vector4 c1;
    public Vector4 c2;
    public Vector4 c3;
    public Vector4 c4;

    public CamDim(Vector4 i1, Vector4 i2, Vector4 i3, Vector4 i4)
    {
        c1 = i1;
        c2 = i2;
        c3 = i3;
        c4 = i4;
    }
    
    public CamDim(Vector4 i1, Vector4 i2, Vector4 i3)
    {
        c1 = i1;
        c2 = i2;
        c3 = i3;
        c4 = default;
    }
    
    public CamDim(Vector4 i1, Vector4 i2)
    {
        c1 = i1;
        c2 = i2;
        c3 = default;
        c4 = default;
    }

}

public enum PlayerSelectCount
{
    two,
    three,
    four,
}