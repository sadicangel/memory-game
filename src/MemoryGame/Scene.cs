using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MemoryGame;

public class SceneManager(ContentManager contentManager)
{
    private readonly Stack<Scene> _sceneStack = new();

    public Scene CurrentScene { get => _sceneStack.Peek(); }

    public void PushScene(Scene scene)
    {
        scene.LoadContent(contentManager);
        _sceneStack.Push(scene);
    }

    public void PopScene() => _sceneStack.Pop().UnloadContent();
}

public abstract class Scene(SceneManager sceneManager) : IDisposable
{
    public SceneManager SceneManager { get; } = sceneManager;

    public virtual void LoadContent(ContentManager contentManager) { }
    public virtual void UnloadContent() { }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

    public void Dispose()
    {
        UnloadContent();
        GC.SuppressFinalize(this);
    }
}