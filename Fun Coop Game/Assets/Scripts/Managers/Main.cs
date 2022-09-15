using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
	/* The Main script is the Main initializer for the backend Gamesystems. this includes but is not limited to:
	 * loading the Gamemanager
     *
     *
     *
     *
     *
     */

    public class Main : MonoBehaviour
    {
        public GameManager GameManager;
        public SceneHandler SceneHandler;
        public GameInput controls;
        public InputAction pause;
        public static Main Instance { get { return instance; } }
        
        private static Main instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            } else {
                instance = this;
            }

            controls = new GameInput();
        }

        
        private void OnEnable()
        {
            pause = controls.UI.Pause;
            
            pause.Enable();
            pause.performed += OnPause;
        }

        private void OnDisable()
        {
            pause.performed -= OnPause;
            pause.Disable();
        }

        public void OnPause(InputAction.CallbackContext obj)
        {
            SceneHandler.Pause();
        }
        
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void LoadMain()
        {
            GameObject main = GameObject.Instantiate(Resources.Load("Main")) as GameObject;
            GameObject.DontDestroyOnLoad(main);
            Debug.Log("Main Loaded");
        }
    }
}
