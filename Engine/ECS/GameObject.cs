using System.Collections.Generic;
using Key_Quest.Engine.ECS.Components;

namespace Key_Quest.Engine.ECS;

public class GameObject
{
    private string _tag;
    
    private List<Component> _components = new List<Component>();

    private Transform _transform;

    public string Tag
    {
        get => _tag;
        set => _tag = value;
    }

    public List<Component> Components
    {
        get => _components;
    }

    public Transform Transform
    {
        get => _transform;
    }
    
    public GameObject()
    {
        _tag = "game_object";
        _transform = new Transform();

        AddComponent(_transform);

        Init();
    }

    protected virtual void Init()
    {
        
    }

    public void AddComponent(Component component)
    {
        component.SetGameObject(this);
        _components.Add(component);
    }

    public void RemoveComponent(Component component)
    {
        _components.Remove(component);
    }
}