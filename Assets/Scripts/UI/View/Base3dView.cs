using UI.Controllers;
using UnityEngine;

namespace UI.View
{
    public abstract class Base3dView<T> : BaseView<T> where T: BaseViewController
    {
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}
