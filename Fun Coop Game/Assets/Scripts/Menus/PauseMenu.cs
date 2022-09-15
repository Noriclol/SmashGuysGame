using Core;
using UnityEngine;

namespace Menus
{
    public class PauseMenu : MonoBehaviour
    {
        private SceneHandler sceneHandler;
        
        private void Awake()
        {
            Time.timeScale = 0f;
            sceneHandler = Main.Instance.SceneHandler;
        }

        // Buttons
        public void Resume()
        {
            Time.timeScale = 1f;
            sceneHandler.Pause();
        }

        public void Quit()
        {
            Time.timeScale = 1f;
            Main.Instance.SceneHandler.isPaused = false;
            sceneHandler.Load("MainMenu");
        }
    }
}
