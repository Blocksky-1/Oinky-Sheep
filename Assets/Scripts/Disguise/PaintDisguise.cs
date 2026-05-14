using UnityEngine;
using UnityEngine.InputSystem;

public class PaintDisguise : MonoBehaviour
{
    [Header("Configuración de Pintura")]
    [SerializeField] private float maxPaint = 100f;
    [SerializeField] private float lossPerSecond = 5f;
    [SerializeField] private float lossPerMovement = 10f;
    [SerializeField] private Color oinkColor = new Color(1f, 0.4f, 0.7f);
    [SerializeField] private Color sheepColor = Color.white;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb2d;

    private float paintAmount;
    public bool isPainted { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        paintAmount = maxPaint;
    }

    private void Start()
    {
        if (paintAmount > 0) isPainted = true;
    }

    private void Update()
    {
        PaintConsumptionHandler();
        VisualUpdater();
    }

    private void PaintConsumptionHandler()
    {
        if (isPainted && paintAmount > 0)
        {
            float gastoTotal = lossPerSecond;

            if (rb2d != null && Mathf.Abs(rb2d.linearVelocity.x) > 0.1f)
            {
                gastoTotal += lossPerMovement;
            }

            paintAmount -= gastoTotal * Time.deltaTime;

            if (paintAmount <= 0)
            {
                paintAmount = 0;
                isPainted = false;
            }
        }
    }

    private void VisualUpdater()
    {
        float lerpFactor = isPainted ? (paintAmount / maxPaint) : 0f;

        spriteRenderer.color = Color.Lerp(sheepColor, oinkColor, lerpFactor);
    }

    public void SwitchingDisguise(InputAction.CallbackContext context)
    {
        if (context.started && paintAmount > 0)
        {
            isPainted = !isPainted;
        }
    }

    public void RefillPaint(float cantidad)
    {
        paintAmount = Mathf.Min(paintAmount + cantidad, maxPaint);

        if (paintAmount > 0.1f)
        {
            isPainted = true;
        }
    }

    public float GetPaintPercentage()
    {
        return paintAmount / maxPaint;
    }
}