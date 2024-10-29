using Key_Quest.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Key_Quest.Sandbox;

public static class Assets
{
    public static class Spritesheets
    {
        public static Spritesheet Knight { get; set; } = new Spritesheet(Config.Content.Load<Texture2D>("sprites/knight_spritesheet"), 3, 4, new Vector2(32, 32));
    }
}