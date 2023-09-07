using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Meta.Loaders.Json
{
    public interface IJsonMeta { }
    public interface IJsonMetaLoader { }
    
    public abstract class BaseMetaLoader
    {
        public abstract UniTask<object> Load(Action<object, DiContainer> callback, DiContainer container);
    }
    
    public abstract class BaseJsonMetaLoader: BaseMetaLoader, IJsonMetaLoader
    {
        public const string resourcesMetaPath = "DB/JsonsMeta";
    }
}
