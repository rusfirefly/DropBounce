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
        SetVibleADS(_isADS);
    }

    private void SetVibleADS(bool visible)
    {
        YandexGame.StickyAdActivity(visible);
        _infoYG.AdWhenLoadingScene = visible;
        _infoYG.showFirstAd = visible;
    }

    public void RemoveADS()
    {
        _isADS = false;
        _saveHandler.SaveBuyNoADS(_isADS);
    }
}
