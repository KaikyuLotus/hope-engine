using System.Collections.Generic;

namespace HopeEngine.Engine.Objects
{
    public class GameObject : Component
    {

        public Transform Transform { get; private set; } = new Transform();

        public List<Component> Components { get; private set; } = new List<Component>();

        public override void OnFingerDown(float x, float y) { }

        public override void OnFingerMove(float x, float y) { }

        public override void OnFingerUp(float x, float y) { }

        internal override void InternalOnFingerDown(float x, float y)
        {
            OnFingerDown(x, y);
            foreach (Component component in Components)
            {
                component.InternalOnFingerDown(x, y);
            }
        }

        internal override void InternalOnFingerMove(float x, float y)
        {
            OnFingerMove(x, y);
            foreach (Component component in Components)
            {
                component.InternalOnFingerMove(x, y);
            }
        }

        internal override void InternalOnFingerUp(float x, float y)
        {
            OnFingerUp(x, y);
            foreach (Component component in Components)
            {
                component.InternalOnFingerUp(x, y);
            }
        }

        internal override void InternalDispose()
        {
            Dispose();
            foreach (Component component in Components)
            {
                component.InternalDispose();
            }
        }

        internal override void InternalPrepare()
        {
            // foreach (var component in Components)
            // {
            //     component.InternalPrepare();
            // }
            Prepare();
        }

        internal override void InternalRender()
        {
            if (Parent != null)
            {
                Transform.ParentPosition = Parent.Transform.GetRelativePosition();
            }
            Render();
            foreach (Component component in Components)
            {
                component.InternalRender();
            }
        }

        internal override void InternalUpdate()
        {
            foreach (Component component in Components)
            {
                component.InternalUpdate();
            }
            Update();
        }

        public void AddComponents(params Component[] components)
        {
            foreach (Component component in components)
            {
                component.Parent = this;
                component.InternalPrepare();
            }
            Components.AddRange(components);
        }

    }
}