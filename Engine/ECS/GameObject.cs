using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using TiledSharp;

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

    public TmxObject MapObject { get; set; }
    
    public GameObject(TmxObject mapObject = null)
    {
        MapObject = mapObject;
        
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

    public bool HasComponent<T>() where T : Component
    {
        return _components.OfType<T>().Any();
    }

    public List<BoxCollider> GetColliders()
    {
        return _colliders;
    }
    
    public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
    {
        Vector2 direction = target - current;
        float distance = direction.Length();

        // If the distance is already smaller than the max delta, return the target
        if (distance <= maxDistanceDelta || distance == 0f)
        {
            return target;
        }

        // Move towards the target by the max delta
        return current + direction / distance * maxDistanceDelta;
    }
}