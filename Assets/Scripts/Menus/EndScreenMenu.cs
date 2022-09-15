using Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Menus
{
    public class EndScreenMenu : MonoBehaviour
    {
        private SceneHandler sceneHandler;

        public Button restartButton;
        public Button menuButton;
        public GameObject VictoryText;
        public GameObject DefeatText;
        public bool Victory = false;

        public void Awake()
        {
            sceneHandler = Main.Instance.SceneHandler;
            VictoryText.gameObject.SetActive(false);
            DefeatText.gameObject.SetActive(false);
            
        }


        public void OnEnable()
        {
            SetCondition();
        }

        public void SetCondition()
        {
            if (Victory)
            {
                VictoryText.gameObject.SetActive(true);
            }
            else
            {
                DefeatText.gameObject.SetActive(true);
            }
        }

        public void Restart()
        {
            sceneHandler.Load("Game");
        }

        public void Menu()
        {
            sceneHandler.Load("Menu");
        }


    }
}
