using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private ColorChange _colorChange;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _currentScoreText;
    [SerializeField] private RectTransform _gameOverWindow;

    private int _currentScore;

    private void OnEnable()
    {
        Player.CollectedCoin += OnCollectedCoin;
        Player.Die += OnDie;
    }

    private void OnDisable()
    {
        Player.CollectedCoin -= OnCollectedCoin;
        Player.Die -= OnDie;
    }

    private void OnDie()
    {
        SetVisibleGameOver(true);
        SetCurrentScore(_currentScore);
        ShowBestScore(YG.YandexGame.savesData.Score);
    }

    public void SetVisibleGameOver(bool isVisible) => _gameOverWindow.gameObject.SetActive(isVisible);

    private void OnCollectedCoin(int score)
    {
        _currentScore = score;
        if (score % 15 == 0 && score != 0)
            _colorChange.SetColor(UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        _scoreText.text = $"{score}";
    }

    public void SetCurrentScore(int score) => _currentScoreText.text = $"{score}";

    public void ShowBestScore(int score)
    {
        _bestScoreText.text = $"{score}";
    }

}
