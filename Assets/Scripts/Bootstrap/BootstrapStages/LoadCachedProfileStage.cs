using System;
using Cysharp.Threading.Tasks;

namespace Startup.BootstrapStages
{
    public class LoadCachedProfileStage: BaseLoadingStage
    {
        public override int stageWeight => 1;

        public override UniTask Run()
        {
            return UniTask.CompletedTask;
        }
    }
}