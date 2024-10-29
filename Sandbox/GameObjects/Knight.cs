using System;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Graphics;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Sandbox.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Key_Quest.Sandbox.GameObjects;

public class Knight : GameObject
{
    protected override void Init()
    {
        base.Init();

        Tag = "player";
        Name = "Knight";

        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Knight, 0) { LayerDepth = 0.1f });
        AddComponent(new BoxCollider(new Vector2(12 * Config.GameScale, 16 * Config.GameScale), new Vector2(10 * Config.GameScale, 9 * Config.GameScale)));
        AddComponent(new Rigidbody());
        AddComponent(new KnightComponent());
        AddComponent(new Animator());

        Animator anim = GetComponent<Animator>();
        anim.AddAnimation("idle", new Animation(Assets.Spritesheets.Knight, new int[] { 0, 1, 2, 3 }, 0.15f));
        anim.AddAnimation("walk", new Animation(Assets.Spritesheets.Knight, new int[] { 4, 5, 6, 7 }, 0.15f));
        anim.AddAnimation("jump", new Animation(Assets.Spritesheets.Knight, new int[] { 8 }, 0.15f));
    }
}