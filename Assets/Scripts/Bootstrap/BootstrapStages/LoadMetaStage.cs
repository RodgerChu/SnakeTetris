using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Meta.Loaders;
using Meta.Loaders.Json;

namespace Startup.BootstrapStages
{
    public class LoadMetaStage: BaseLoadingStage
    {
        public override int stageWeight => 1;
        
        public override UniTask Run()
        {
            var types = Assembly.GetAssembly(typeof(BaseProtoMetaLoader)).GetTypes();
            var loadingOperations = new List<UniTask>();
            foreach (var type in types)
            {
                if (!type.IsAbstract && typeof(IProtoMetaLoader).IsAssignableFrom(type))
                {
                    var loader = Activator.CreateInstance(type) as BaseMetaLoader;
                    var loadingStatus = loader.Load((meta, container) =>
                    {
                        var metaType = meta.GetType();
                        container.Bind(metaType).FromInstance(meta);
                    }, m_container);
                    loadingOperations.Add(loadingStatus.AsUniTask());
                }
            }
            
            return UniTask.WhenAll(loadingOperations);
        }
    }
}