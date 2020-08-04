using HopeEngine.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HopeEngine.Engine.Scenes
{
    public class Scene : IScene
    {
        internal List<GameObject> GameObjects = new List<GameObject>();

        public virtual void Setup()
        {

        }

        public List<GameObject> FindGameObjects(Func<GameObject, bool> filter)
        {
            return GameObjects.Where(filter).ToList();
        }

        public virtual void AddGameObjects(params GameObject[] gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.InternalPrepare();
            }
            GameObjects.AddRange(gameObjects);
        }

    }
}