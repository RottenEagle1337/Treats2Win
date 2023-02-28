using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public Button modeButton, settingsButton, backButton, backButton_1, infinityModeButton, normalModeButton;
    public GroupBox mainMenu, modeMenu, settingsMenu;
    public Slider volumeSlider;
    private float volume;

    private VisualElement root;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        modeButton = root.Q<Button>("mode-button");
        settingsButton = root.Q<Button>("settings-button");
        backButton = root.Q<Button>("back-button");
        backButton_1 = root.Q<Button>("back-1-button");
        infinityModeButton = root.Q<Button>("infinity-mode-button");
        normalModeButton = root.Q<Button>("normal-mode-button");

        modeButton.clicked += OpenModeMenu;
        settingsButton.clicked += OpenSettingsMenu;
        backButton.clicked += OpenMainMenu;
        backButton_1.clicked += OpenMainMenu;
        normalModeButton.clicked += SetNormalMode;
        infinityModeButton.clicked += SetInfinityMode;

        mainMenu = root.Q<GroupBox>("main-menu");
        modeMenu = root.Q<GroupBox>("mode-menu");
        settingsMenu = root.Q<GroupBox>("settings-menu");

        volume = AudioManager.Instance.FindSound("theme").volume;
        volumeSlider = root.Q<Slider>("volume-slider");
        volumeSlider.value = volume;
    }

    private void Update()
    {
        UpdateVolume();
    }

    void OpenSettingsMenu()
    {
        mainMenu.style.display = DisplayStyle.None;
        settingsMenu.style.display = DisplayStyle.Flex;
    }

    void OpenMainMenu()
    {
        settingsMenu.style.display = DisplayStyle.None;
        modeMenu.style.display = DisplayStyle.None;
        mainMenu.style.display = DisplayStyle.Flex;
    }

    void OpenModeMenu()
    {
        mainMenu.style.display = DisplayStyle.None;
        modeMenu.style.display = DisplayStyle.Flex;
    }

    void SetNormalMode()
    {
        GameManager.Instance.mode = Mode.Normal;
        SceneManager.LoadScene("Perestroika");
    }

    void SetInfinityMode()
    {
        GameManager.Instance.mode = Mode.Infinity;
        SceneManager.LoadScene("Perestroika");
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