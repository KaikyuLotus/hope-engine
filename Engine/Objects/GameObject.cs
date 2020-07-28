namespace HopeEngine.Engine.Objects
{
    public class GameObject : IGameObject
    {

        public Transform Transform { get; private set; } = new Transform();

        public virtual void Dispose() { }

        public virtual void Prepare() { }

        public virtual void Render() { }

        public virtual void Update() { }

        public virtual void OnFingerDown(float x, float y) { }

        public virtual void OnFingerMove(float x, float y) { }

        public virtual void OnFingerUp(float x, float y) { }

    }
}