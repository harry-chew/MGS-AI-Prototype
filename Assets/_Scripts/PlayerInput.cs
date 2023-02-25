using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Vector2 _inputVector;
    
    private void Update()
    {
        _inputVector.x = Input.GetAxis("Horizontal");
        _inputVector.y = Input.GetAxis("Vertical");

        _inputVector = _inputVector.normalized;
    }

    public Vector2 GetInputVector()
    {
        return _inputVector;
    }
}
