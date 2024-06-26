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

    [SerializeField] private Sprite _soundOnImage;
    [SerializeField] private Sprite _soundOffImage;
    [SerializeField] private Image _soundImage;

    [SerializeField] private Button _removeADSButton;
    [SerializeField] private Image _removeADSImage;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Sprite _imageSold;

    [SerializeField] private RectTransform _authWindow;
    [SerializeField] private RectTransform _authOk;
    [SerializeField] private RectTransform _authNo;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerScore;
    [SerializeField] private Button _authButton;
    [SerializeField] private LoadingImage _loadingImage;

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
        ShowBestScore(YandexGame.savesData.Score);
        ChangeImageSound(YandexGame.savesData.IsSound);
    }

    public void SetVisibleGameOver(bool isVisible) => _gameOverWindow.gameObject.SetActive(isVisible);

    private void OnCollectedCoin(int score)
    {
        _currentScore = score;
        if (score % 10 == 0 && score != 0)
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

    public void ShowWindowPayments()
    {
        _payments.gameObject.SetActive(true);
    }

    public void ShowSoldWindow() => _sold.gameObject.SetActive(true);

    public void SetSoldButton()
    {
        _removeADSButton.interactable = false;
        _removeADSImage.sprite = _imageSold;
    }

    public void ShowAuthWindow()
    {
        if(_authWindow.gameObject.activeInHierarchy == false)
        {
            _authWindow.gameObject.SetActive(true);
        }

        bool noAuth = true;
        bool auth = false;

        if (YandexGame.auth)
        {
            noAuth = false;
            auth = true;
        }

        _authOk.gameObject.SetActive(auth);
        _authNo.gameObject.SetActive(noAuth);

        _playerName.text = $"{YandexGame.playerName}\n";
        _playerScore.text = $"{YandexGame.savesData.Score}";


        _authButton.interactable = noAuth;

        if(auth)
        {
            //_loadYG.urlImage = YandexGame.playerPhoto;
            //_loadYG.Load();
            _loadingImage.LoadImage(YandexGame.playerPhoto);
        }
    }

    public void StartAuthDialog() => YandexGame.AuthDialog();

    public void ChangeImageSound(bool isSound)
    {
        Sprite image = _soundOffImage;

        if(isSound)
            image = _soundOnImage;

        _soundImage.sprite = image;
    }

    public void LoadInfoPrice(string price)
    {
        _price.text = price;
    }
}
