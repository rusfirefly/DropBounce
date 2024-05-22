using UnityEngine;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
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
        _scoreText.text = $"{_score}";
    }
}
