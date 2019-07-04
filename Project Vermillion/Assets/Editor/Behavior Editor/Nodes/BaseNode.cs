using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.BehaviorEditor
{
    public abstract class BaseNode : ScriptableObject
    {
        public Rect windowRect;

        public string windowTitle;

        public virtual void DrawWindow()
        {

        }

        public virtual void DrawCurve()
        {

        }
    }
}
