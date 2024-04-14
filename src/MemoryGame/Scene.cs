using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MemoryGame;

public class SceneManager(IServiceProvider services)
{
    private readonly Stack<Scene> _sceneStack = new();

    public Scene CurrentScene { get => _sceneStack.Peek(); }

    public TScene GetCurrentScene<TScene>() where TScene : Scene => (TScene)CurrentScene;

    public void PushScene<TScene>() where TScene : Scene
    {
        var scene = services.GetRequiredService<TScene>();
        scene.LoadContent();
        _sceneStack.Push(scene);
    }

    public void PopScene() => _sceneStack.Pop().UnloadContent();
}

public abstract class Scene
{
    public virtual void LoadContent() { }
    public virtual void UnloadContent() { }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(GameTime gameTime) { }
}