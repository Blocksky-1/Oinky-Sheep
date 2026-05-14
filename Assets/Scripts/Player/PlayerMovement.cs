using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    private Rigidbody2D rb2d;
    private float horizontalMovement;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb2d.linearVelocity.y);
    }
    void Update()
    {
        if (horizontalMovement != 0)
        {
            float direction = horizontalMovement > 0 ? 0 : 180;
            transform.rotation = Quaternion.Euler(0, direction, 0);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalMovement = inputVector.x;
    }
}