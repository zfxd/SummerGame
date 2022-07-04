using UnityEngine;

namespace Combat
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;
        public void SetState(State state)
        {
            State = state;
            StartCoroutine(State.Start());
        }

        public void ResumeState(State state)
        {
            State = state;
        }
    }
}