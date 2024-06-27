using UnityEngine;
using UnityEngine.UI;
using YG;

public class Languge : MonoBehaviour
{
    [SerializeField] private Image[] _languageSelected;

    public void CheakLanguage()
    {
        SwitchLanguage(YandexGame.savesData.language);
    }

    private void OnEnable() => YandexGame.SwitchLangEvent += SwitchLanguage;

    private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

    public void SwitchLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                SelectLanguage(0);
                break;
            case "tr":
                SelectLanguage(1);
                break;
            case "en":
                SelectLanguage(2);
                break;
        }

        //YandexGame.SaveProgress();
    }

    private void SelectLanguage(int id)
    {
        bool visible = false;
        for (int i = 0; i < _languageSelected.Length; i++)
        {
            if (i == id)
            {
                visible = true;
            }
            else
            {
                visible = false;
            }

            _languageSelected[i].gameObject.SetActive(visible);
        }
    }

    
}
