using System;
using Key_Quest.Engine;
using Key_Quest.Engine.Input;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Key_Quest;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = Config.WindowWidth;
        _graphics.PreferredBackBufferHeight = Config.WindowHeight;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        Config.Batch = _spriteBatch;
        Config.Content = Content;
        Config.Graphics = GraphicsDevice;
        Config.LoadContent();
        
        SceneManager.AddScene("default", new DefaultScene());
        SceneManager.AddScene("main_menu", new MainMenuScene());
        SceneManager.ChangeScene("default");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Config.Time = gameTime;
        
        KeyboardHandler.GetState();
        
        if (KeyboardHandler.IsPressed(Keys.Q))
        {
            SceneManager.ChangeScene("default");
        }
        else if (KeyboardHandler.IsPressed(Keys.E))
        {
            SceneManager.ChangeScene("main_menu");
        }
        
        SceneManager.UpdateCurrentScene();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        
        Config.Batch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);
        SceneManager.RenderCurrentScene();
        Config.Batch.End();
        
        base.Draw(gameTime);
    }
}