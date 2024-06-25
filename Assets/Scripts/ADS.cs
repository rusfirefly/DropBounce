using UnityEngine;
using YG;
using YG.Utils.Pay;

public class ADS : MonoBehaviour
{
    [SerializeField] private InfoYG _infoYG;
    [SerializeField] private HudHandler _hudHandler;
    [SerializeField] private LoadingImage _loadingImage;
    [SerializeField] private ConsumePurchasesYG _consumePurchasesYG;

    private SaveHandler _saveHandler;
    private bool _isADS;

    public void Initialize(SaveHandler saveHandler)
    {
        _saveHandler = saveHandler;
        _isADS = _saveHandler.IsADS;
        SetVibleADS(_isADS);
        
    }

    private void OnEnable()
    {
        YandexGame.GetPaymentsEvent += OnPaymentsEvent;
    }

    private void OnDisable()
    {
        YandexGame.GetPaymentsEvent -= OnPaymentsEvent;
    }

    private void OnPaymentsEvent()
    {
        _hudHandler.LoadInfoPrice("update");
        Purchase[] purchases = YandexGame.purchases;
        foreach (Purchase purchase in purchases)
        {
            _loadingImage.LoadImage(purchase.imageURI);
            _hudHandler.LoadInfoPrice($"{purchase.priceValue}");
        }
        
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

    public void LoadInfo()
    {
        
       /* Purchase[] purchases = YandexGame.purchases;
        foreach (Purchase purchase in purchases)
        {
            _loadingImage.LoadImage(purchase.imageURI);
            _hudHandler.LoadInfoPrice($"{purchase.priceValue}");
        }*/
    }

}
