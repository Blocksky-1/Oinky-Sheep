using UnityEngine;

public class PigAI : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float chaseDistance = 5f;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private PaintDisguise playerPaint;
    private Vector2 moveDirection;
    private bool isChasing = false;

    public void SetChasing(bool status)
    {
        isChasing = status;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerPaint = player.GetComponent<PaintDisguise>();
        }
    }

    private void Update()
    {
        if (playerTransform == null || playerPaint == null) return;

        PigDetection detectionScript = GetComponent<PigDetection>();
        float dynamicRange = (detectionScript != null) ?
                             detectionScript.GetCurrentDetectionRange() :
                             chaseDistance;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        bool paintExpired = !playerPaint.isPainted;

        if (isChasing || (distanceToPlayer < dynamicRange && paintExpired))
        {
            float dirX = (playerTransform.position.x - transform.position.x);
            moveDirection = new Vector2(Mathf.Sign(dirX), 0);

            if (moveDirection.x != 0)
            {
                float angle = moveDirection.x > 0 ? 0 : 180;
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
        else
        {
            moveDirection = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}