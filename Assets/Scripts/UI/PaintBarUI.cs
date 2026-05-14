using UnityEngine;
using UnityEngine.UI;

public class PaintBarUI : MonoBehaviour
{
    [SerializeField] private PaintDisguise playerPaint;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color barColor = new Color(1f, 0.4f, 0.7f);

    private void Start()
    {
        if (fillImage != null)
        {
            fillImage.color = barColor;
        }
    }

    private void Update()
    {
        if (playerPaint != null && fillImage != null)
        {
            fillImage.fillAmount = playerPaint.GetPaintPercentage();

            // Opcional: Que la barra desaparezca si no estás disfrazado
            // fillImage.enabled = playerPaint.isPainted;
        }
    }
}