using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Sandbox.GameObjects;

namespace Key_Quest.Sandbox.Components;

public class DoorComponent : Component
{
    private SpriteRenderer _sr;

    public bool Locked = true;
    
    public override void OnStart()
    {
        base.OnStart();

        _sr = GameObject.GetComponent<SpriteRenderer>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        _sr.SpriteIndex = Locked ? 0 : 1;
    }
}