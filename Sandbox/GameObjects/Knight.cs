using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Key_Quest.Sandbox.GameObjects;

public class Knight : GameObject
{
    public Knight(Vector2 initialPosition)
    {
        Transform.Position = initialPosition;
    }
    
    protected override void Init()
    {
        base.Init();

        Tag = "player";
        Name = "Knight";

        Transform.Scale = new Vector2(Config.GameScale);

        Spritesheet knightSpritesheet = new Spritesheet(Assets.Sprites.KnightSpritesheet, 3, 4, new Vector2(32, 32));
        
        AddComponent(new SpriteRenderer(knightSpritesheet, 0));
    }
}