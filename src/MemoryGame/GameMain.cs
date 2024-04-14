using MemoryGame.CardGame;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MemoryGame;
public class GameMain : Game
{
    private GraphicsDeviceManager _graphics;
    private ServiceProvider _services = null!;
    private SceneManager _sceneManager = null!;

    public GameMain()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    public new IServiceProvider Services { get => _services; }

    protected override void Initialize()
    {
        _services = new ServiceCollection()
            .AddSingleton(this)
            .AddSingleton(Content)
            .AddSingleton(_graphics)
            .AddSingleton(new SpriteBatch(GraphicsDevice))
            .AddSingleton<SceneManager>()
            .AddSingleton<CardScene>()
            .BuildServiceProvider();

        _sceneManager = _services.GetRequiredService<SceneManager>();
        _sceneManager.PushScene<CardScene>();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TODO: Load main window content.
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _sceneManager.CurrentScene.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _sceneManager.CurrentScene.Draw(gameTime);

        base.Draw(gameTime);
    }
}
