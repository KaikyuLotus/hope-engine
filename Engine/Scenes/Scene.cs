using System.Collections.Generic;
using HopeEngine.Engine.Objects;

namespace HopeEngine.Engine.Scenes
{
    public class Scene : IScene
    {
        internal List<GameObject> GameObjects = new List<GameObject>();

        public virtual void Setup()
        {

        }

        public virtual void AddGameObjects(params GameObject[] gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Prepare();
            }
            GameObjects.AddRange(gameObjects);
        }

    }
}