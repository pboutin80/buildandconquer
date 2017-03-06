using Core.Managers;
using UnityEngine;

namespace UI.Menu.Controllers
{
    public class MenuController : MonoBehaviour
    {
        public Animator m_InitialOpen;

        private GameScreenManager m_ScreenManager;

        void Awake()
        {
            m_ScreenManager = Managers.Get<GameScreenManager>();
            if (m_InitialOpen)
            {
                m_ScreenManager.OpenPanel(m_InitialOpen);
            }
        }
    }
}
