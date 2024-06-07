using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ColorChange _colorChange;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private RectTransform _gameOverWindow;
    [SerializeField] private RectTransform _payments;
    [SerializeField] private Button _removeADSButton;

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
    
    public void ShowWindowPayments(float scale, float duration)
    {
        _payments.transform.DOScale(scale, duration);
    }

    public void HideBuyADSButton()=> _removeADSButton.gameObject.SetActive(false);  
}
