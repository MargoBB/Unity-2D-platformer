using UnityEngine;

public class ShowCanvasOnCollision : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        // Переконайтеся, що канвас спочатку невидимий
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Перевірка, чи зіткнення відбулося з гравцем
        if (collision.CompareTag("Player"))
        {
            // Відображення канвасу
            canvas.SetActive(true);
        }
    }
}
