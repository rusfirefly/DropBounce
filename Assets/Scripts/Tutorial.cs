using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private RectTransform[] _tutorials;
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private GameObject _eggFatigue;
    [SerializeField] private TMP_Text _caption;

    private SaveHandler _saveHandler;
    private int _numbreTutorial;

    private void Update()
    {
        if (_numbreTutorial >= _tutorials.Length)
        {
            //_closeButton.gameObject.SetActive(true);
            //Time.timeScale = 0;
            HideTutorial();
            return;
        }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Ended && _numbreTutorial == 0)
            {
                NextTutorial();
            }

            if (touch.phase == TouchPhase.Began && _numbreTutorial >= 1 && _numbreTutorial != 2)
            {
                NextTutorial();
            }
            
            if (_numbreTutorial == 1)
            {
                Invoke("ShowEgg", 1f);
            }
            else
            {
                SetVisibleEggFatigue(false);
            }

        }
    }

    private void OnEnable()
    {
        Player.CollectedCoin += OnCollectedCoin;
    }

    private void OnCollectedCoin(int coin)
    {
        if (_numbreTutorial != 2) return;
        NextTutorial();
        Player.CollectedCoin -= OnCollectedCoin;
    }

    public void Close()
    {
        Time.timeScale = 1;
        HideTutorial();
    }
    
    public void Initialized(SaveHandler saveHandler)
    {
        _saveHandler = saveHandler;

        SetHideTutorial(saveHandler.IsTutorial);

        if (saveHandler.IsTutorial)
        {
            _numbreTutorial = 0;
            _caption.gameObject.SetActive(true);
            _tutorials[_numbreTutorial].gameObject.SetActive(true);
        }
       
    }

    private void HideTutorial()
    {
        _caption.gameObject.SetActive(false);
        _saveHandler.SetHideTutorial();
        _tutorial.gameObject.SetActive(false);
    }
    
    private void SetHideTutorial(bool isTutorial)
    {
        _tutorial.SetActive(isTutorial);
    }

    private void NextTutorial()
    {
        _tutorials[_numbreTutorial].gameObject.SetActive(false);
        _numbreTutorial++;
        if(_numbreTutorial < _tutorials.Length)
            _tutorials[_numbreTutorial].gameObject.SetActive(true);

    }

    private void SetVisibleEggFatigue(bool visible) => _eggFatigue.SetActive(visible);
    private void ShowEgg()=> _eggFatigue.SetActive(true);
}
