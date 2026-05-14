using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    [SerializeField] private float refillAmount = 40f;
    [SerializeField] private bool destroyOnUse = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PaintDisguise disguiseSystem = collision.GetComponent<PaintDisguise>();

        if (disguiseSystem == null)
        {
            disguiseSystem = collision.GetComponentInParent<PaintDisguise>();
        }

        if (disguiseSystem != null)
        {
            disguiseSystem.RefillPaint(refillAmount);
            //Debug.Log("Paint Refill");

            if (destroyOnUse)
            {
                Destroy(gameObject);
            }
        }
    }
}