using UnityEngine;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private ColorChange _colorChange;
    private float _score;

    private void OnEnable()
    {
        Player.CollectedCoin += OnCollectedCoin;
    }

    private void OnDisable()
    {
        Player.CollectedCoin -= OnCollectedCoin;
    }

    private void OnCollectedCoin()
    {
        _score++;
        if (_score % 2 == 0 && _score != 0)
            _colorChange.SetColor(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        _scoreText.text = $"{_score}";
    }
}
