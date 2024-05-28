using UnityEngine;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private ColorChange _colorChange;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _currentScoreText;

    private void OnEnable()
    {
        Player.CollectedCoin += OnCollectedCoin;
    }

    private void OnDisable()
    {
        Player.CollectedCoin -= OnCollectedCoin;
    }

    private void OnCollectedCoin(int score)
    {
        if (score % 20 == 0 && score != 0)
            _colorChange.SetColor(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        _scoreText.text = $"{score}";
    }

    public void SetCurrentScore(int score) => _currentScoreText.text = $"{score}";

    public void ShowBestScore(int score)
    {
        _bestScoreText.text = $"{score}";
    }

}
