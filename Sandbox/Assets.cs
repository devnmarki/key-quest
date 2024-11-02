using System;
using Key_Quest.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Key_Quest.Sandbox;

public static class Assets
{
    public static class Spritesheets
    {
        public static Spritesheet Knight { get; set; } = new Spritesheet(Config.Content.Load<Texture2D>("sprites/knight_spritesheet"), 3, 4, new Vector2(32));
        public static Spritesheet Door { get; set; } = new Spritesheet(Config.Content.Load<Texture2D>("sprites/objects/door_spritesheet"), 1, 2, new Vector2(16));
        
        public static class Enemies
        {
            public static Spritesheet UndeadKnight { get; set; } = new Spritesheet(Config.Content.Load<Texture2D>("sprites/enemies/undead_knight_spritesheet"), 3, 4, new Vector2(32));
        }
    }

    public static class Sprites
    {
        public static class Items
        {
            public static Texture2D Key { get; set; } = Config.Content.Load<Texture2D>("sprites/items/key");
            public static Texture2D Shield { get; set; } = Config.Content.Load<Texture2D>("sprites/items/weapons/shield");
        }
    }
}