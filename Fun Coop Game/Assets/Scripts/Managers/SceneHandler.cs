using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Core
{
    /* SceneHandler is responsible for instantiation and management of scenes
     * 
     * 
     */ 
    public class SceneHandler : MonoBehaviour
    {
        public bool isPaused = false;
        
        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            //Scene s = SceneManager.GetSceneByName(sceneName);
            //SceneManager.SetActiveScene(s);
        }
        public void Switch(string newSceneName, string oldSceneName)
        {
            SceneManager.LoadScene(newSceneName);
            Scene s = SceneManager.GetSceneByName(newSceneName);
            SceneManager.SetActiveScene(s);
            SceneManager.UnloadSceneAsync(oldSceneName);
        }

        public void Unload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        [ContextMenu("Pause")]
        public void Pause()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Debug.Log("MainMenu is active");
                return; 
            }
            
            if (isPaused)
            {
                isPaused = false;
                SceneManager.UnloadSceneAsync("PauseMenu");

            }
            else
            {
                isPaused = true;
                SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            }
        }


    }
}
