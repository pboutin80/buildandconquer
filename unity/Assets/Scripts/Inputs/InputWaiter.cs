using UnityEngine;

namespace Core.Inputs
{
    public class InputWaiter : MonoBehaviour
    {
        private bool m_ReceivedInput;

        public bool ReceivedInput
        {
            get { return m_ReceivedInput; }
        }

        private void Update()
        {
            if (!m_ReceivedInput && Input.anyKey)
            {
                m_ReceivedInput = true;
            }
        }
    }
}
