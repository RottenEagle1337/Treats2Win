using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class InGameMenuUIManager : MonoBehaviour
{
    public Button resumeButton, settingsButton, backButton, quitButton, quitButton_1, quitButton_2;
    public GroupBox inGameMenu, pauseMenu, winMenu, deathMenu, pauseMainMenu, pauseSettingsMenu;
    public Label scoreText, hpText, deathScoreText;
    public Slider volumeSlider;

    private VisualElement root;
    private bool isPaused;
    private float volume;

    private void Start()
    {
        UnityEngine.Cursor.visible = false;

        isPaused = false;
        Time.timeScale = 1f;

        root = GetComponent<UIDocument>().rootVisualElement;

        resumeButton = root.Q<Button>("continue-button");
        settingsButton = root.Q<Button>("settings-button");
        backButton = root.Q<Button>("back-button");
        quitButton = root.Q<Button>("quit-button");
        quitButton_1 = root.Q<Button>("quit-1-button");
        quitButton_2 = root.Q<Button>("quit-2-button");

        resumeButton.clicked += OpenInGameMenu;
        settingsButton.clicked += OpenPauseSettingsMenu;
        backButton.clicked += OpenPauseMainMenu;
        quitButton.clicked += OpenMainMenuScene;
        quitButton_1.clicked += OpenMainMenuScene;
        quitButton_2.clicked += OpenMainMenuScene;


        inGameMenu = root.Q<GroupBox>("ingame-menu");
        pauseMenu = root.Q<GroupBox>("pause-menu");
        winMenu = root.Q<GroupBox>("win-menu");
        deathMenu = root.Q<GroupBox>("death-menu");
        pauseMainMenu = root.Q<GroupBox>("pause-main-menu");
        pauseSettingsMenu = root.Q<GroupBox>("pause-settings-menu");

        scoreText = root.Q<Label>("score-text");
        hpText = root.Q<Label>("hp-text");
        deathScoreText = root.Q<Label>("death-score-text");

        volume = AudioManager.Instance.FindSound("theme").volume;
        volumeSlider = root.Q<Slider>("volume-slider");
        volumeSlider.value = volume;
    }

    private void Update()
    {
        PauseGame();

        UpdateText();
        UpdateState();
        UpdateVolume();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !PlayerController.Instance.isDead)
        {
            isPaused = true;
            Time.timeScale = 0f;
            OpenPauseMenu();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            OpenInGameMenu();
        }
    }


    void OpenPauseMenu()
    {
        UnityEngine.Cursor.visible = true;
        inGameMenu.style.display = DisplayStyle.None;
        pauseMenu.style.display = DisplayStyle.Flex;
        pauseMainMenu.style.display = DisplayStyle.Flex;
    }

    void OpenInGameMenu()
    {
        UnityEngine.Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1f;

        pauseMenu.style.display = DisplayStyle.None;
        pauseSettingsMenu.style.display = DisplayStyle.None;
        inGameMenu.style.display = DisplayStyle.Flex;
    }

    void OpenWinMenu()
    {
        UnityEngine.Cursor.visible = true;
        inGameMenu.style.display = DisplayStyle.None;
        winMenu.style.display = DisplayStyle.Flex;
    }

    void OpenDeathMenu()
    {
        UnityEngine.Cursor.visible = true;
        deathScoreText.text = "Your Score: " + PlayerController.Instance.candies;
        inGameMenu.style.display = DisplayStyle.None;
        deathMenu.style.display = DisplayStyle.Flex;
    }

    void OpenPauseSettingsMenu()
    {
        pauseMainMenu.style.display = DisplayStyle.None;
        pauseSettingsMenu.style.display = DisplayStyle.Flex;
    }

    void OpenPauseMainMenu()
    {
        pauseSettingsMenu.style.display = DisplayStyle.None;
        pauseMainMenu.style.display = DisplayStyle.Flex;
    }

    void OpenMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void UpdateText()
    {
        scoreText.text = "Score: " + PlayerController.Instance.candies;
        hpText.text = PlayerController.Instance.healthPoints.ToString();
    }

    void UpdateState()
    {
        if (PlayerController.Instance.isWin)
            OpenWinMenu();
        else if (PlayerController.Instance.isDead)
            OpenDeathMenu();
    }
    void UpdateVolume()
    {
        if (volume != volumeSlider.value)
        {
            volume = volumeSlider.value;
            AudioManager.Instance.ChangeVolumeTheme(volume);
        }
    }
}
