using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private VisualElement eogElement;
    private Label score;
    private Label ball;
    private Label label;
    private Button menuButton;

    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        score = root.Q<Label>("Score");
        ball = root.Q<Label>("Ball");
        eogElement = root.Q<VisualElement>("EndOfGame");
        label = eogElement.Q<Label>();
        menuButton = eogElement.Q<Button>();

        eogElement.style.display = DisplayStyle.None;
        eogElement.style.visibility = Visibility.Hidden;
    }

    private void OnEnable()
    {
        menuButton.clicked += OnMenuButtonClicked;
        GameEvents.ScoreCalculatedEvent += OnScoreCalculatedEventCalled;
        GameEvents.EndOfGameEvent += OnEndOfGameEventCalled;
        GameEvents.BallNumberChangedEvent += OnBallNumberChangedEventCalled;
    }

    private void OnDisable()
    {
        menuButton.clicked -= OnMenuButtonClicked;
        GameEvents.ScoreCalculatedEvent -= OnScoreCalculatedEventCalled;
        GameEvents.EndOfGameEvent -= OnEndOfGameEventCalled;
        GameEvents.BallNumberChangedEvent -= OnBallNumberChangedEventCalled;
    }

    private void OnBallNumberChangedEventCalled(object sender, int arg)
    {
        ball.text = "Ball: " + arg.ToString();
    }

    private void OnEndOfGameEventCalled(object sender, bool arg)
    {
        eogElement.style.display = DisplayStyle.Flex;
        eogElement.style.visibility = Visibility.Visible;

        label.text = arg ? "VICTORY" : "DEFEAT";
    }

    private void OnScoreCalculatedEventCalled(object sender, float arg)
    {
        score.text = "Score: " + arg.ToString();
    }

    private void OnMenuButtonClicked()
    {
        GameEvents.GameReset();
        SceneManager.LoadScene("MenuScene");
    }
}
