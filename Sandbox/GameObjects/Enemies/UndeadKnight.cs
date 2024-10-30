using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Sandbox.Components.Enemies;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.GameObjects.Enemies;

public class UndeadKnight : GameObject
{
    protected override void Init()
    {
        base.Init();

        Tag = "enemy";
        Name = "Undead Knight";

        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Enemies.UndeadKnight, 0) { LayerDepth = 0.1f });
        AddComponent(new BoxCollider(new Vector2(12 * Config.GameScale, 16 * Config.GameScale)));
        AddComponent(new Rigidbody());
        AddComponent(new EnemyComponent());
        AddComponent(new UndeadKnightComponent());
        
        GetComponent<SpriteRenderer>().SpriteOffset = new Vector2(10 * Config.GameScale, 9 * Config.GameScale);
        GetComponent<Rigidbody>().UseGravity = false;
    }
}