using Key_Quest.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace Key_Quest.Sandbox;

public static class Assets
{
    public static class Sprites
    {
        public static Texture2D KnightSpritesheet { get; set; } = Config.Content.Load<Texture2D>("sprites/knight_spritesheet");
    }
}