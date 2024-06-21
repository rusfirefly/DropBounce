using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class LoadingImage : MonoBehaviour
{
    
    public static Action<Image> OnLoadImage;
    [SerializeField] private Image _spriteImage;

    public void LoadImage(string url)
    {
        _spriteImage.enabled = false;
        StartCoroutine(SwapPlayerPhoto(url));
    }

    private IEnumerator SwapPlayerPhoto(string url)
    {
#if UNITY_2020_1_OR_NEWER
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                DownloadHandlerTexture handlerTexture = webRequest.downloadHandler as DownloadHandlerTexture;

                if (_spriteImage)
                {
                    if (handlerTexture.isDone)
                        _spriteImage.sprite = Sprite.Create((Texture2D)handlerTexture.texture,
                            new Rect(0, 0, handlerTexture.texture.width, handlerTexture.texture.height), Vector2.zero);

                    OnLoadImage?.Invoke(_spriteImage);
                    _spriteImage.enabled = true;
                }

            }
        }
#endif

    }
}
