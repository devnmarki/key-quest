namespace Key_Quest.Engine.ECS.Components;

public class Component
{
    protected GameObject GameObject { get; private set; }
    
    public Component()
    {
        
    }

    public void SetGameObject(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public virtual void OnStart()
    {
        
    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual void OnDraw()
    {
        
    }
}