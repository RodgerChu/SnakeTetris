using System;
using System.Collections.Generic;
using General;

namespace Messaging
{
    public interface IEventBussEventData { }
    
    public class EventBuss: Singleton<EventBuss>
    {
        private class SingleDataEventBuss<TStruct> where TStruct: struct, IEventBussEventData
        {
            public Action<TStruct> listeners;

            public void FireEvent(in TStruct data) => listeners?.Invoke(data);
        }
        
        private Dictionary<Type, object> m_eventBusses = new Dictionary<Type, object>();

        public void SubscribeToEvent<TStruct>(Action<TStruct> callback) where TStruct: struct, IEventBussEventData
        {
            var t = typeof(TStruct);
            if (!m_eventBusses.TryGetValue(t, out var eventBussObj))
            {
                var bussType = typeof(SingleDataEventBuss<>).MakeGenericType(t);
                eventBussObj = Activator.CreateInstance(bussType);
                m_eventBusses.Add(t, eventBussObj);
            }

            var eventBuss = eventBussObj as SingleDataEventBuss<TStruct>;
            eventBuss.listeners += callback;
        }

        public void UnsubscribeSubscribeToEvent<TStruct>(Action<TStruct> callback)
            where TStruct : struct, IEventBussEventData
        {
            var structType = typeof(TStruct);
            if (!m_eventBusses.TryGetValue(structType, out var eventBussObj))
            {
                return;
            }

            var eventBuss = eventBussObj as SingleDataEventBuss<TStruct>;
            eventBuss.listeners -= callback;
        }

        public void FireEvent<TStruct>(in TStruct data) where TStruct: struct, IEventBussEventData
        {
            var structType = typeof(TStruct);
            if (!m_eventBusses.TryGetValue(structType, out var eventBussObj))
            {
                return;
            }
                
            var eventBuss = eventBussObj as SingleDataEventBuss<TStruct>;
            eventBuss.FireEvent(in data);
        }
    }
}