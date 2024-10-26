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
        foreach (GameObject gameObject in _gameObjects)
        {
            foreach (Component component in gameObject.Components)
            {
                component.OnStart();
            }
        }
    }

    public virtual void Update()
    {
        List<GameObject> gameObjectsCopy = new List<GameObject>(_gameObjects);
        foreach (GameObject gameObject in gameObjectsCopy)
        {
            List<Component> componentsCopy = new List<Component>(gameObject.Components);
            foreach (Component component in componentsCopy)
            {
                component.OnUpdate();
            }
        }
    }

    public virtual void Render()
    {
        List<GameObject> gameObjectsCopy = new List<GameObject>(_gameObjects);
        foreach (GameObject gameObject in gameObjectsCopy)
        {
            List<Component> componentsCopy = new List<Component>(gameObject.Components);
            foreach (Component component in componentsCopy)
            {
                component.OnDraw();
            }
        }
    }
}