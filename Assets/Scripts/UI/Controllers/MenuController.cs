using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private Button playButton;
    private Button quitButton;
    private Label highscore;

    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        playButton = root.Q<Button>("PlayButton");
        quitButton = root.Q<Button>("QuitButton");
        highscore = root.Q<Label>("Highscore");
    }

    private void Start()
    {
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
    }

    private void OnEnable()
    {
        playButton.clicked += OnPlayButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnDisable()
    {
        playButton.clicked -= OnPlayButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
