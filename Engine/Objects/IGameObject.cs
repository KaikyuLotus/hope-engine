namespace HopeEngine.Engine.Objects
{
    public interface IGameObject
    {

        public void Update();

        public void Render();

        public void Prepare();

        public void Dispose();

        public void OnFingerDown(float x, float y);

        public void OnFingerUp(float x, float y);

        public void OnFingerMove(float x, float y);

    }
}