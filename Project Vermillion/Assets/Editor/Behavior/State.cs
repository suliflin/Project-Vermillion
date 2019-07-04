using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu]
    public class State : ScriptableObject
    {
        public List<Transition> transitions = new List<Transition>();

        public void Tick()
        {

        }

        public Transition AddTransition()
        {
            Transition retVal = new Transition();
            transitions.Add(retVal);
            return retVal;
        }
    }
}