using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Sandbox.Components;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.GameObjects;

public class Ladders : GameObject
{
    protected override void Init()
    {
        base.Init();
        
        AddComponent(new BoxCollider(Vector2.Zero));
        AddComponent(new LaddersComponent());
    }
}