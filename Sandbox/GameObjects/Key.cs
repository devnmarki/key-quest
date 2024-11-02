using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.GameObjects;

public class Key : GameObject
{
    protected override void Init()
    {
        base.Init();

        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Sprites.Items.Key) { LayerDepth = Globals.Layers.Objects });
        AddComponent(new BoxCollider(new Vector2(4 * Config.GameScale, 8 * Config.GameScale), new Vector2(6 * Config.GameScale, 4 * Config.GameScale)));
    }
}