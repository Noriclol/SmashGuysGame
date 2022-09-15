using System;
using Core;
using TMPro;
using UnityEngine;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        private SceneHandler sceneHandler;
        public TMP_Dropdown LevelSelect;
        public TMP_Dropdown PlayersSelect;
        
        
        private void Awake()
        {
            sceneHandler = Main.Instance.SceneHandler;
        }



        // Buttons
        public void Play()
        {
            string SelectedLevel = "World 1";
            switch (PlayersSelect.value)
            {
                case 0:
                    Main.Instance.GameManager.players = PlayerSelectCount.two;
                    break;
                
                case 1:
                    Main.Instance.GameManager.players = PlayerSelectCount.three;
                    break;
                
                case 2:
                    Main.Instance.GameManager.players = PlayerSelectCount.four;
                    break;
            }
            switch (LevelSelect.value)
            {
                case 0:
                    SelectedLevel = "World 1";
                    break;
                
                case 1:
                    SelectedLevel = "World 2";
                    break;
                
                case 2:
                    SelectedLevel = "World 3";
                    break;
            }
            sceneHandler.Load(SelectedLevel);
        }
        
        

        public void Exit()
        {
            Application.Quit();
        }
    }
    
    
    
    
    
}
