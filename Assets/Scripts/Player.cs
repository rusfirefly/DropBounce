using UnityEngine;
using DG.Tweening;
using System;
using YG;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static event Action<int> CollectedCoin;
    public static event Action Die;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private CoinPool _coinPool;
    [SerializeField] private Ease _effect;
    [SerializeField] private Transform _reloadDrop;
    [SerializeField] private ParticleSystem _effectDie;

    [SerializeField] private AudioSource _audioDrop;
    [SerializeField] private AudioSource _audioCollected;
    [SerializeField] private AudioSource _audioGameOver;
    [SerializeField] private SaveHandler _saveHandler;

    private TrailRenderer _trailRender;
    private int _score;
    private float _yHomePosition;
    private float _currentTime;
    private float _timeDrop = 5f;
    private Vector3 _scale;
    private bool _isDrope;
    private bool _isHome;
    private bool _isDie;

    private void Start()
    {
        _trailRender = GetComponent<TrailRenderer>();
        _saveHandler = FindAnyObjectByType<SaveHandler>();

        _isHome = true;
        OnNewGame();
    }

    private void Update()
    {
        if (_isDrope == true) return;

        _currentTime += Time.deltaTime;
        _scale.x = _scale.y = (9 - _currentTime) / 3f;
        _reloadDrop.localScale = _scale;

        if (_currentTime >= _timeDrop)
        {
            _move.DropCube(TouchPhase.Ended);
            _isDrope = true;
            _currentTime = 0;
        }
    }

    private void OnEnable()
    {
        _move.Drop += OnDrop;
       // LevelHandler.NewGame += OnNewGame;
    }

    private void OnDisable()
    {
        _move.Drop -= OnDrop;
        //LevelHandler.NewGame -= OnNewGame;
    }

    private void OnNewGame()
    {
        _yHomePosition = transform.position.y;
        _score = 0;
        _scale = new Vector3(3, 3, 0.1f);
        _reloadDrop.localScale = _scale;
        if (_effectDie)
        {
            if (_effectDie.isPlaying)
                _effectDie.Stop();
        }
    }

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            transform.DOMoveY(_yHomePosition, 0.2f).SetEase(_effect);
            _isHome = false;
        }
        else
        if (collision.gameObject.tag == "Home")
        {
            if (_isHome == false)
            {
                _reloadDrop.gameObject.SetActive(true);
                _rigidbody.isKinematic = true;
                _move.IsDroped = false;
                _isDrope = false;
                _currentTime = 0;
                _isHome = true;
            }
        }
        else
        if (collision.gameObject.tag == "Enemy")
        {
            _isDie = true;
            _effectDie.transform.position = collision.gameObject.transform.position;
            gameObject.transform.localScale = Vector3.zero; 

            if (_trailRender)
                _trailRender.enabled = false;

            if (_effectDie.isPlaying == false)
                _effectDie.Play();

            PlaySound(_audioGameOver);
            Invoke("OnDie",2);
        }
    }

    private void OnDie()
    {
        YandexGame.LoadProgress();
        //YandexGame.LoadCloud();
        int bestScore = YandexGame.savesData.Score;

        if (_score > bestScore)
        {
            YandexGame.NewLeaderboardScores("Score", _score);
            _saveHandler.SaveScore(_score);
        }

        Die?.Invoke();
        Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDie) return;

        if(other.gameObject.tag == "Coin")
        {
            PlaySound(_audioCollected);
            _score++;
            _coinPool.ReturnObjecToPool(other.gameObject);
            CollectedCoin?.Invoke(_score);
        }
    }

    private void OnDrop()
    {
        PlaySound(_audioDrop);
        _reloadDrop.gameObject.SetActive(false);
        if (_rigidbody)
        {
            _rigidbody.isKinematic = false;
        }
        
    }

    private void PlaySound(AudioSource source)
    {
        if(source.isPlaying == false)
            source.Play();
    }
}
