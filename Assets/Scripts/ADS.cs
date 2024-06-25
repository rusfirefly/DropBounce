using TMPro;
using Unity.VisualScripting;
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
        if (YandexGame.savesData.IsADS)
        {
            _hudHandler.ShowWindowPayments();
            LoadPurchases();
        }
        else
        {
            _hudHandler.ShowSoldWindow();
        }
    }

    private void LoadPurchases()
    {
        YandexGame.GetPayments();

        Purchase[] purchases = YandexGame.purchases;
        foreach (Purchase purchase in purchases)
        {
            _loadingImage.LoadImage(purchase.currencyImage);
            _hudHandler.LoadInfoPrice($"{purchase.price}");
        }
    }
}
