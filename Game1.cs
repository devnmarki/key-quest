using System;
using Key_Quest.Engine;
using Key_Quest.Engine.Input;
using Key_Quest.Engine.LevelEditor;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox;
using Key_Quest.Sandbox.GameObjects;
using Key_Quest.Sandbox.GameObjects.Enemies;
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
        
        Assets.Sounds.LoadSounds();
        
        SceneManager.AddScene("default", new DefaultScene());
        SceneManager.AddScene("main_menu", new MainMenuScene());
        SceneManager.AddScene("level_2", new Level2Scene());
        
        TilemapManager.AddGameObjectToLoad("Knight", () => new Knight());
        TilemapManager.AddGameObjectToLoad("Undead Knight", () => new UndeadKnight());
        TilemapManager.AddGameObjectToLoad("Key", (() => new Key()));
        TilemapManager.AddGameObjectToLoad("Door", () => new Door());
        TilemapManager.AddGameObjectToLoad("Ladders", () => new Ladders());
        
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
        
        if (KeyboardHandler.IsPressed(Keys.Tab))
            Config.DebugMode = !Config.DebugMode;
        
        if (KeyboardHandler.IsPressed(Keys.R))
            SceneManager.RefreshCurrentScene();
        
        SceneManager.UpdateCurrentScene();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(new Vector3(36/255f, 44/255f, 66/255f)));
        
        Config.Batch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointWrap);
        SceneManager.RenderCurrentScene();
        Config.Batch.End();
        
        base.Draw(gameTime);
    }
}