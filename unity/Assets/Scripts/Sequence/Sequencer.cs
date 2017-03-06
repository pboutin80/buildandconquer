
using System;
using System.Collections.Generic;

namespace Core.Sequence
{
    public class Sequencer
    {
        private Sequence m_Sequence;

        public Sequencer()
        {
            m_Sequence = new Sequence();
        }

        public void AddNode()
        {

        }
    }

    public class Sequence : SequencePlayable
    {
        private readonly List<SequencePlayable> m_Nodes = new List<SequencePlayable>();
        private SequencePlayable m_CurrentNode;

        protected override void OnPlay()
        {
            if (m_CurrentNode == null)
            {
                m_CurrentNode = m_Nodes[0];
            }

            m_CurrentNode.Play();
        }

        protected override void OnStop()
        {
            if (m_CurrentNode != null)
            {
                m_CurrentNode.Stop();
            }
        }

        protected override void OnPause()
        {
            if (m_CurrentNode != null)
            {
                m_CurrentNode.Pause();
            }
        }

        protected override void OnUpdate()
        {
            if (m_CurrentNode == null) return;

            m_CurrentNode.Update();

            SequencePlayable nextNode;
            if (EvaluateOut(out nextNode))
            {
                m_CurrentNode.Stop();
                nextNode.Play();

                m_CurrentNode = nextNode;
            }
        }
    }

    public class SequenceNode<T> : SequencePlayable
    {

    }

    public abstract class SequencePlayable : IPlayableNode
    {
        private bool m_Playing;
        private bool m_Paused;

        public float Time { get; set; }
        public bool Paused
        {
            get { return m_Paused; }
            set
            {
                if (value) Pause();
                else Play();
            }
        }
        public bool Playing
        {
            get { return m_Playing; }
            set
            {
                if (value) Play();
                else Stop();
            }
        }

        public IList<NodeTransition> Outs { get; set; }

        public void Play()
        {
            if (Playing) return;

            Paused = false;
            Playing = true;

            OnPlay();
        }

        public void Stop()
        {
            if (!Playing) return;

            Time = 0;
            Playing = false;

            OnStop();
        }

        public void Pause()
        {
            if (!Paused) return;

            Paused = true;
            Playing = false;

            OnPause();
        }

        public void Update()
        {
            if (!Playing) return;

            OnUpdate();
        }

        protected virtual void OnPlay()
        {

        }

        protected virtual void OnStop()
        {

        }

        protected virtual void OnPause()
        {

        }

        protected virtual void OnUpdate()
        {

        }

        protected bool EvaluateOut(out SequencePlayable aNextNode)
        {
            aNextNode = null;
            for (int i = 0; i < Outs.Count; i++)
            {
                if (Outs[i].TestCondition())
                {
                    aNextNode = Outs[i].NextNode;
                    return true;
                }
            }
            return false;
        }
    }

    public class NodeTransition
    {
        public SequencePlayable CurrentNode { get; set; }
        public SequencePlayable NextNode { get; set; }
        public Predicate<SequencePlayable> Condition { get; set; }

        public bool TestCondition()
        {
            return Condition(CurrentNode);
        }
    }

    public interface IPlayable
    {
        float Time { get; set; }
        bool Paused { get; set; }
        bool Playing { get; set; }
        void Play();
        void Stop();
        void Pause();
        void Update();
    }

    public interface IPlayableNode : IPlayable
    {
        IList<NodeTransition> Outs { get; set; }
    }
}
