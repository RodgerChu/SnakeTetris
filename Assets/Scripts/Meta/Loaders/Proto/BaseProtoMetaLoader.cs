using Meta.Loaders.Json;

namespace Meta.Loaders
{
    public interface IProtoMeta { }
    public interface IProtoMetaLoader { }
    
    public abstract class BaseProtoMetaLoader: BaseMetaLoader, IProtoMetaLoader
    {
        public const string resourcesMetaPath = "DB/ProtoMeta";
    }
}
