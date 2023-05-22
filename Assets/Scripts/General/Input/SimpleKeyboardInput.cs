using UnityEngine;

public class SimpleKeyboardInput : MonoBehaviour, IMovementInput
{
    public Vector2Int GetInput()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        return new Vector2Int((int)x, -(int)y);
    }
}
