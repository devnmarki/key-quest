using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;

namespace Key_Quest.Engine.ECS;

public class GameObject
{
    private string _tag;
    private string _name;
    
    private List<Component> _components = new List<Component>();
    private List<BoxCollider> _colliders = new List<BoxCollider>();

    private Transform _transform;

    public string Tag
    {
        get => _tag;
        set => _tag = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
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
        _name = "Game Object";
        
        _transform = new Transform();

        AddComponent(_transform);

        Init();
    }

    protected virtual void Init()
    {
        
    }

    public void AddComponent(Component component)
    {
        component.GameObject = this;
        _components.Add(component);

        if (component is BoxCollider collider)
        {
            _colliders.Add(collider);
        }
    }

    public void RemoveComponent(Component component)
    {
        _components.Remove(component);
    }

    public T GetComponent<T>() where T : Component
    {
        foreach (var component in _components)
        {
            if (component is T target)
            {
                return target;
            }
        }

        return null;
    }

    public List<BoxCollider> GetColliders()
    {
        return _colliders;
    }
}