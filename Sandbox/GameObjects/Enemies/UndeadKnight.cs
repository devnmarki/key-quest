using System;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Graphics;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox.Components.Enemies;
using Key_Quest.Sandbox.GameObjects.Items.Weapons;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.GameObjects.Enemies;

public class UndeadKnight : GameObject
{
    private Shield _shield = null;
    
    protected override void Init()
    {
        base.Init();

        Tag = "enemy";
        Name = "Undead Knight";

        Transform.Scale = new Vector2(Config.GameScale);
        
        CreateShield();
        
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Enemies.UndeadKnight, 0) { LayerDepth = 500 });
        AddComponent(new BoxCollider(new Vector2(12 * Config.GameScale, 16 * Config.GameScale)));
        AddComponent(new Rigidbody());
        AddComponent(new Animator());
        AddComponent(new EnemyComponent());
        AddComponent(new UndeadKnightComponent() { Shield = _shield });
        
        GetComponent<SpriteRenderer>().SpriteOffset = new Vector2(10 * Config.GameScale, 9 * Config.GameScale);
        GetComponent<Rigidbody>().UseGravity = false;
        
        Animator anim = GetComponent<Animator>();
        anim.AddAnimation("idle", new Animation(Assets.Spritesheets.Enemies.UndeadKnight, new int[] { 0, 1, 2, 3 }, 0.15f));
        anim.AddAnimation("walk", new Animation(Assets.Spritesheets.Enemies.UndeadKnight, new [] { 4, 5, 6, 7 }, 0.15f));
    }

    private void CreateShield()
    {
        _shield = new Shield()
        {
            Transform =
            {
                Position = Transform.Position,
                Scale = new Vector2(Config.GameScale)
            }
        };
        
        SceneManager.CurrentScene.AddGameObject(_shield);
    }
}