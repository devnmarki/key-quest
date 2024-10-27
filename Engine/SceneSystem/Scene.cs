using System.Collections.Generic;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;

namespace Key_Quest.Engine.SceneSystem;

public class Scene
{
    private List<GameObject> _gameObjects = new List<GameObject>();

    public List<GameObject> GameObjects
    {
        get => _gameObjects;
    }
    
    public Scene()
    {
        
    }

    protected void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    protected void RemoveGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }
    
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            foreach (Component component in gameObject.Components)
            {
                component.OnUpdate();
            }
        }
    }

    public virtual void Render()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            foreach (Component component in gameObject.Components)
            {
                component.OnDraw();
            }
        }
    }
}