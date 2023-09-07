using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Startup.BootstrapStages
{
    public abstract class BaseLoadingStage
    {
        [Inject] protected DiContainer m_container;
        
        public abstract int stageWeight { get; }
        public abstract UniTask Run();
    }
}