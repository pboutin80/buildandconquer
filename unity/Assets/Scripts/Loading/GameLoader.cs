using Core.Inputs;
using System;
using System.Collections;
using UnityEngine;

namespace Core.Loading
{
    public class GameLoader : MonoBehaviour
    {
        public event Action LoadComplete;
        public event Action<float> LoadProgress;

        public InputWaiter m_InputWaiter;

        public void StartLoad()
        {
            StartCoroutine(LoadingSequence());
        }

        private IEnumerator LoadingSequence()
        {
            yield return LoadAppDependencies();
            yield return DisplayWaiter();

            if (LoadComplete != null)
            {
                LoadComplete();
            }
        }

        private IEnumerator LoadAppDependencies()
        {
            yield return new WaitForSeconds(3F);
        }

        private IEnumerator DisplayWaiter()
        {
            m_InputWaiter.gameObject.SetActive(true);

            while (!m_InputWaiter.ReceivedInput)
            {
                yield return null;
            }

            m_InputWaiter.gameObject.SetActive(false);
        }
    }
}