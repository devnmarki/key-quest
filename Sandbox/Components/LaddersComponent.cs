using Key_Quest.Engine;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.Components;

public class LaddersComponent : Component
{
    private BoxCollider _collider;
    
    public override void OnStart()
    {
        base.OnStart();

        _collider = GameObject.GetComponent<BoxCollider>();
        _collider.Size = new Vector2((float)GameObject.MapObject.Width * Config.GameScale, (float)GameObject.MapObject.Height * Config.GameScale);
    }
}