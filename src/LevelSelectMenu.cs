using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    void Start()
    {
        // Завантажуємо стан відкритих рівнів
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        // Встановлюємо інтерактивність кнопок в залежності від відкритих рівнів
        level1Button.interactable = levelReached >= 1;
        level2Button.interactable = levelReached >= 2;
        level3Button.interactable = levelReached >= 3;

        // Додаємо обробники натискань на кнопки
        level1Button.onClick.AddListener(() => LoadLevel(1));
        level2Button.onClick.AddListener(() => LoadLevel(2));
        level3Button.onClick.AddListener(() => LoadLevel(3));
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Lvl" + levelIndex);
    }
}
