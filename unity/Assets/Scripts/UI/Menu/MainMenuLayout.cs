using Core.Managers;
using UI.Attributes;

namespace UI.Menu
{
    public class MainMenuLayout : MenuLayout
    {
        [GUIAction("NewGame")]
        private void NewGame()
        {
            Managers.Get<GameManager>().NewGame();
        }

        [GUIAction("LoadGame")]
        private void LoadGame()
        {
            Managers.Get<GameManager>().LoadGames();
        }

        [GUIAction("LoadGame", true)]
        private bool LoadGame_Enabled()
        {
            return Managers.Get<SaveGameStorage>().HasSaveGames();
        }

        [GUIAction("Options")]
        private void Options()
        {

        }

        [GUIAction("ExitGame")]
        private void ExitGame()
        {
            Managers.Get<GameManager>().ExitGame();
        }
    }
}
