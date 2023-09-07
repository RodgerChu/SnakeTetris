using System;
using System.Collections.Generic;
using GameCore.Systems.Base;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace GameCore.Systems
{
    public class SystemsEarlyUpdate { }
    public class SystemsLateUpdate { }
    public class SystemsFrameEnd { }
    
    public class GameTickSystem
    {
        private List<BaseGameSystem> m_gameSystems = new List<BaseGameSystem>(15);

        public GameTickSystem()
        {
            var defaultSystems = PlayerLoop.GetDefaultPlayerLoop();

            var systemsEarlyUpdate = new PlayerLoopSystem
            {
                subSystemList = null,
                updateDelegate = SystemsEarlyUpdate,
                type = typeof(SystemsEarlyUpdate)
            };
            var systemsLateUpdate = new PlayerLoopSystem
            {
                subSystemList = null,
                updateDelegate = SystemsLateUpdate,
                type = typeof(SystemsLateUpdate)
            };
            var systemsFrameCleanup = new PlayerLoopSystem
            {
                subSystemList = null,
                updateDelegate = SystemsOnFrameEnd,
                type = typeof(SystemsFrameEnd)
            };
            

            var newLoop = AddSystem<PreLateUpdate>(in defaultSystems, systemsLateUpdate);
            newLoop = AddSystem<PreUpdate>(in newLoop, systemsEarlyUpdate);
            newLoop = AddSystem<PreUpdate>(in newLoop, systemsEarlyUpdate);
            newLoop = AddSystem<PostLateUpdate>(in newLoop, systemsFrameCleanup);
            PlayerLoop.SetPlayerLoop(newLoop);
        }

        public void AddSystem(BaseGameSystem system)
        {
            m_gameSystems.Add(system);
        }

        private void SystemsOnFrameEnd()
        {
            foreach (var system in m_gameSystems)
            {
                system.OnFrameEnd();
            }
        }

        private void SystemsLateUpdate()
        {
            foreach (var system in m_gameSystems)
            {
                system.OnSystemsLateUpdate();
            }
        }

        private void SystemsEarlyUpdate()
        {
            foreach (var system in m_gameSystems)
            {
                system.OnSystemsEarlyUpdate();
            }
        }

        private static PlayerLoopSystem AddSystem<T>(in PlayerLoopSystem loopSystem, PlayerLoopSystem systemToAdd) where T : struct
        {
            PlayerLoopSystem newPlayerLoop = new()
            {
                loopConditionFunction = loopSystem.loopConditionFunction,
                type = loopSystem.type,
                updateDelegate = loopSystem.updateDelegate,
                updateFunction = loopSystem.updateFunction
            };

            List<PlayerLoopSystem> newSubSystemList = new();

            foreach (var subSystem in loopSystem.subSystemList)
            {
                newSubSystemList.Add(subSystem);
      
                if (subSystem.type == typeof(T))
                    newSubSystemList.Add(systemToAdd);
            }

            newPlayerLoop.subSystemList = newSubSystemList.ToArray();
            return newPlayerLoop;
        }
    }
}