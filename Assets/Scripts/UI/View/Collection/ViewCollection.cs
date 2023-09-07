using UnityEngine;

namespace UI.View.Collection
{
    [CreateAssetMenu(menuName = "View/Collection", fileName = "ViewCollection", order = 1)]
    public class ViewCollection : ScriptableObject
    {
        public BaseView[] viewPrefabs;
    }
}
