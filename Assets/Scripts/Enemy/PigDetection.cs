using UnityEngine;

public class PigDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float baseDetectionRange = 4f;
    [SerializeField] private float maxDetectionRange = 8f;
    [SerializeField] private float detectionSpeed = 1.5f;

    [Header("Visual References")]
    [SerializeField] private SpriteRenderer alertIcon;
    [SerializeField] private Color safeColor = Color.white;
    [SerializeField] private Color alertColor = Color.yellow;

    private PaintDisguise playerPaint;
    private Transform playerTransform;
    private float alertLevel = 0f;
    private bool isChasing = false;

    private PigAI pigMovement;

    private void Awake()
    {
        pigMovement = GetComponent<PigAI>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerPaint = player.GetComponent<PaintDisguise>();
        }

        if (alertIcon != null) alertIcon.enabled = false;
    }

    private void Update()
    {
        if (playerTransform == null || playerPaint == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        float paintFactor = 1f - playerPaint.GetPaintPercentage();
        float currentDetectionRange = Mathf.Lerp(baseDetectionRange, maxDetectionRange, paintFactor);

        if (distance <= currentDetectionRange)
        {
            HandleDetection(paintFactor);
        }
        else
        {
            CoolDownDetection();
        }

        UpdateAlertVisuals();
    }

    private void HandleDetection(float paintFactor)
    {
        if (isChasing) return;

        if (alertIcon != null) alertIcon.enabled = true;

        float multiplier = 1f + (paintFactor * 2f);
        alertLevel += Time.deltaTime * detectionSpeed * multiplier;

        if (alertLevel >= 1f)
        {
            alertLevel = 1f;
            isChasing = true;
            if (pigMovement != null) pigMovement.SetChasing(true);
        }
    }

    private void CoolDownDetection()
    {
        if (alertLevel > 0)
        {
            alertLevel -= Time.deltaTime * 0.5f;
        }
        else
        {
            if (alertIcon != null) alertIcon.enabled = false;
            isChasing = false;
            if (pigMovement != null) pigMovement.SetChasing(false);
        }
    }
    public float GetCurrentDetectionRange()
    {
        if (playerPaint == null) return baseDetectionRange;

        float paintFactor = 1f - playerPaint.GetPaintPercentage();
        return Mathf.Lerp(baseDetectionRange, maxDetectionRange, paintFactor);
    }
    private void UpdateAlertVisuals()
    {
        if (alertIcon != null && alertIcon.enabled)
        {
            alertIcon.color = Color.Lerp(safeColor, alertColor, alertLevel);

            float scale = Mathf.Lerp(0.5f, 1.2f, alertLevel);
            alertIcon.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        if (playerPaint == null) return;

        Gizmos.color = Color.yellow;
        float paintFactor = 1f - playerPaint.GetPaintPercentage();
        float currentRange = Mathf.Lerp(baseDetectionRange, maxDetectionRange, paintFactor);
        Gizmos.DrawWireSphere(transform.position, currentRange);
    }
}