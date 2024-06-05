using UnityEngine;
using YG;

public class ADS : MonoBehaviour
{
    [SerializeField] private InfoYG _infoYG;
    private SaveHandler _saveHandler;
    private bool _isADS;

    public void Initialize(SaveHandler saveHandler)
    {
        _saveHandler = saveHandler;
        _isADS = _saveHandler.IsADS;

        if(_isADS == false)
            YandexGame.StickyAdActivity(true);

        SetVisibleADS();
    }


    private void SetVisibleADS()
    {
        _infoYG.AdWhenLoadingScene = false;
        _infoYG.showFirstAd = false;
    }

}
