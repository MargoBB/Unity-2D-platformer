using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructionsPanel;  // Основна панель з інструкціями
    public Button okButton;               // Кнопка "OK"
    public GameObject[] instructionPages; // Масив сторінок з інструкціями
    public Button nextButton;             // Кнопка для переходу на наступну сторінку
    public Button prevButton;             // Кнопка для повернення на попередню сторінку
    private int currentPageIndex = 0;     // Індекс поточної сторінки

    void Start()
    {
        // Перевірка чи гравець вже бачив інструкції
        if (PlayerPrefs.GetInt("HasSeenInstructions", 0) == 0)
        {
            ShowInstructions();
        }
        else
        {
            instructionsPanel.SetActive(false);
        }

        // Прив'язка кнопок до методів
        okButton.onClick.AddListener(CloseInstructions);
        nextButton.onClick.AddListener(ShowNextPage);
        prevButton.onClick.AddListener(ShowPreviousPage);

        // Початкова ініціалізація сторінок
        UpdatePage();
    }

    void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
        Time.timeScale = 0; // Зупинка гри
    }

    void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
        Time.timeScale = 1; // Продовження гри
        PlayerPrefs.SetInt("HasSeenInstructions", 1);
        PlayerPrefs.Save();
    }

    void ShowNextPage()
    {
        if (currentPageIndex < instructionPages.Length - 1)
        {
            currentPageIndex++;
            UpdatePage();
        }
    }

    void ShowPreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePage();
        }
    }

    void UpdatePage()
    {
        for (int i = 0; i < instructionPages.Length; i++)
        {
            instructionPages[i].SetActive(i == currentPageIndex);
        }

        prevButton.gameObject.SetActive(currentPageIndex > 0);
        nextButton.gameObject.SetActive(currentPageIndex < instructionPages.Length - 1);
    }
}
