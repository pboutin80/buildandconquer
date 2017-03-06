using System;
using Core.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameObject m_GameLoaderPrefab;

        private GameLoader m_GameLoader;

        void Awake()
        {
            if (m_GameLoaderPrefab)
            {
                var loader = Instantiate(m_GameLoaderPrefab);
                if (loader)
                {
                    m_GameLoader = loader.GetComponent<GameLoader>();
                    if (m_GameLoader)
                    {
                        m_GameLoader.LoadComplete += GameLoader_LoadComplete;
                        m_GameLoader.StartLoad();
                    }
                }
            }
        }

        private void GameLoader_LoadComplete()
        {
            m_GameLoader.LoadComplete -= GameLoader_LoadComplete;

            SceneManager.sceneLoaded += MenuSceneLoaded;
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
        }

        private void MenuSceneLoaded(Scene aScene, LoadSceneMode aMode)
        {
            SceneManager.sceneLoaded -= MenuSceneLoaded;

            Destroy(m_GameLoader.gameObject);
        }

        public void NewGame()
        {
            Debug.Log("New Game...");
        }

        public void LoadGames()
        {

        }

        public void ExitGame()
        {
            Debug.Log("Exit Game...");
            Application.Quit();
        }
    }
}
