using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using YG;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ColorChange _colorChange;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private RectTransform _gameOverWindow;
    [SerializeField] private RectTransform _payments;
    [SerializeField] private RectTransform _sold;

    [SerializeField] private RectTransform _soudnOnOff;

    [SerializeField] private Button _removeADSButton;
    [SerializeField] private Image _removeADSImage;
    [SerializeField] private Sprite _imageSold;

    [SerializeField] private RectTransform _authWindow;
    [SerializeField] private RectTransform _authOk;
    [SerializeField] private RectTransform _authNo;
    [SerializeField] private TMP_Text _authInfo;
    [SerializeField] private Button _authButton;
    [SerializeField] private ImageLoadYG _loadYG;
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

    public void ShowSoldWindow() => _sold.gameObject.SetActive(true);

    public void SetSoldButton()
    {
        _removeADSButton.interactable = false;
        _removeADSImage.sprite = _imageSold;
    }

    public void ShowAuthWindow()
    {
        bool noAuth = true;
        bool auth = false;

        if (YandexGame.auth)
        {
            noAuth = false;
            auth = true;
            _authInfo.text = $"Login: {YandexGame.playerName}\n" +
                             $"Score: {YandexGame.savesData.Score}";

        }

        _authOk.gameObject.SetActive(auth);
        _authNo.gameObject.SetActive(noAuth);
        _authButton.interactable = noAuth;

        if(auth)
        {
           _loadYG.urlImage = YandexGame.playerPhoto;
           _loadYG.Load();
        }
    }

    public void StartAuthDialog() => YandexGame.AuthDialog();
}
