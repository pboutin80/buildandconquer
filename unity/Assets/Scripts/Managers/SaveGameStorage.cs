using Core.Save;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers
{
    public class SaveGameStorage : MonoBehaviour
    {
        public event Action<int, SaveGame> GameLoaded;
        public event Action<int> GameSaved;

        private readonly List<SaveGame> m_SaveGames = new List<SaveGame>();

        public bool HasSaveGames()
        {
            return m_SaveGames.Count > 0;
        }

        public bool LoadSaveGame(int aIndex)
        {
            return true;
        }

        public bool SaveGame(int aIndex)
        {
            return true;
        }
    }
}
