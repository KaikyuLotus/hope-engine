using HopeEngine.Engine.Objects;

namespace HopeEngine.Engine.Scenes
{
    interface IScene
    {

        public void Setup();

        public void AddGameObjects(params GameObject[] gameObjects);

    }
}