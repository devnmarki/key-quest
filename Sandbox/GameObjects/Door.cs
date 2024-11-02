using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Sandbox.Components;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.GameObjects;

public class Door : GameObject
{
    protected override void Init()
    {
        base.Init();

        Name = "Door";
        Tag = "door";
        
        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Door, 0) { LayerDepth = Globals.Layers.Objects });
        AddComponent(new BoxCollider(new Vector2(8 * Config.GameScale), new Vector2(4 * Config.GameScale)));
        AddComponent(new DoorComponent());
    }
}