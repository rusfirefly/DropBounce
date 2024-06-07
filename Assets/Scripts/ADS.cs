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

        if (_isADS == false)
        {
            HideADS();
        }
    }

    private void HideADS()
    {
        YandexGame.StickyAdActivity(false);
        _infoYG.AdWhenLoadingScene = false;
        _infoYG.showFirstAd = false;
    }

    public void RemoveADS()
    {
        _isADS = true;
        _saveHandler.SaveBuyNoADS();
    }
}
