using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Key_Quest.Engine;

public class Config
{
    public static SpriteBatch Batch { get; set; }
    public static ContentManager Content { get; set; }
    public static GraphicsDevice Graphics { get; set; }

    public static int WindowWidth { get; set; } = 1280;
    public static int WindowHeight { get; set; } = 720;
    public static GameTime Time { get; set; }

    public static Vector2 GravityScale { get; set; } = new Vector2(0f, 9.81f);
    
    public static Texture2D PixelTexture { get; private set; }
    
    public static void LoadContent()
    {
        PixelTexture = new Texture2D(Graphics, 1, 1);
        PixelTexture.SetData(new[] { Color.White });
    }
}