using System.Collections.Generic;
using GameCore.Debug;
using GameCore.Pooling;
using UI.View;
using UI.View.Collection;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ViewSystem
    {
        [Inject] private Canvas m_canvas;
        [Inject] private ViewCollection m_viewCollection;
        [Inject] private MonoPool<BaseView> m_viewPool;
        [Inject] private ConsoleLogger m_logger;

        private List<BaseView> m_loadedViews = new List<BaseView>();

        public T Show<T>() where T : BaseView, new()
        {
            foreach (var viewPrefab in m_viewCollection.viewPrefabs)
            {
                if (viewPrefab is T tPrefab)
                {
                    var view = m_viewPool.Get(tPrefab);
                    view.gameObject.SetActive(true);
                    view.transform.SetParent(m_canvas.transform);
                    return view;
                }
            }
            
            m_logger.LogError("Cannot find view with type " + typeof(T).Name);
            return null;
        }

        public void Hide<T>() where T : BaseView, new()
        {
            T viewToRemove = null;
            foreach (var view in m_loadedViews)
            {
                if (view is T tView)
                {
                    viewToRemove = tView;
                    break;
                }
            }

            if (viewToRemove is null)
            {
                return;
            }
            
            m_viewPool.Return(viewToRemove);
            m_loadedViews.Remove(viewToRemove);
        }

        public void HideAll()
        {
            foreach (var view in m_loadedViews)
            {
                m_viewPool.Return(view);
            }
            
            m_loadedViews.Clear();
        }
    }
}