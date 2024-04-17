using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace MemoryGame.Common;

public sealed class ResourceCache(ContentManager contentManager) : IDisposable
{
    private readonly ConcurrentDictionary<string, object> _resources = new();

    public TResource GetResource<TResource>(string name) => (TResource)_resources.GetOrAdd(name, n => contentManager.Load<TResource>(n)!);

    public void Dispose()
    {
        var keys = _resources.Keys.ToList();
        foreach (var key in keys)
            if (_resources.TryRemove(key, out var texture))
                if (texture is IDisposable disposable)
                    disposable.Dispose();
    }
}
