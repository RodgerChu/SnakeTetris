using System;
using System.Collections.Generic;
using System.Reflection;
using GameCore.Pooling;
using Startup.BootstrapStages;
using States.Abstraction;
using UI.View.Bootstrap;
using Zenject;

namespace GameCore.States.Concrete
{
    public class BootstrapState: BaseState
    {
        [Inject] private DiContainer m_container;
        [Inject] private GameStateSystem m_gameStateSystem;
        [Inject] private Pool<BaseState> m_statesPool;
        
        private List<BaseLoadingStage> m_loadingStages = new(5);
        
        public override void PrepareForActivation(Action<BaseState> onReady)
        {
            m_viewManager.Show<BootstrapView>();
            base.PrepareForActivation(onReady);
        }

        public override void Activate()
        {
            var loadingStagesType = typeof(BaseLoadingStage);
            foreach (var type in Assembly.GetAssembly(loadingStagesType).GetTypes())
            {
                if (!type.IsAbstract && type.IsClass && loadingStagesType.IsAssignableFrom(type))
                {
                    m_loadingStages.Add(m_container.Instantiate(type) as BaseLoadingStage);
                }
            }

            StartLoading();
        }

        public override void Deactivate()
        {
        }
        
        private async void StartLoading()
        {
            foreach (var stage in m_loadingStages)
            {
                await stage.Run();
            }
            
            m_gameStateSystem.SwitchTo(m_statesPool.Get<GameStartState>());
        }
    }
}