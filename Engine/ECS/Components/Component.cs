using System;

namespace Key_Quest.Engine.ECS.Components;

public class Component
{
    public GameObject GameObject { get; internal set; }
    
    public Component()
    {
        
    }

    public virtual void OnStart()
    {
        if (GameObject == null)
        {
            Console.WriteLine("Game Object is null in current component! Component: " + this);
            return;
        }   
    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual void OnDraw()
    {
        
    }
}