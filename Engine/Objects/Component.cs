namespace HopeEngine.Engine.Objects
{
    public class Component : IGameObject
    {

        public GameObject Parent { internal set; get; }

        public virtual void OnFingerDown(float x, float y) { }

        public virtual void OnFingerMove(float x, float y) { }

        public virtual void OnFingerUp(float x, float y) { }

        public virtual void Dispose() { }

        public virtual void Prepare() { }

        public virtual void Render() { }

        public virtual void Update() { }

        internal virtual void InternalOnFingerDown(float x, float y)
        {
            OnFingerDown(x, y);
        }

        internal virtual void InternalOnFingerMove(float x, float y)
        {
            OnFingerMove(x, y);
        }

        internal virtual void InternalOnFingerUp(float x, float y)
        {
            OnFingerUp(x, y);
        }

        internal virtual void InternalDispose()
        {
            Dispose();
        }

        internal virtual void InternalPrepare()
        {
            Prepare();
        }

        internal virtual void InternalRender()
        {
            if (this is GameObject)
            {
                GameObject thisObj = (GameObject)this;
                if (Parent != null)
                {
                    thisObj.Transform.ParentPosition = Parent.Transform.GetRelativePosition();
                }
            }
            Render();
        }

        internal virtual void InternalUpdate()
        {
            Update();
        }
    }
}