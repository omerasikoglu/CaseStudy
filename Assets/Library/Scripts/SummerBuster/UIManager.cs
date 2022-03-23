using UnityEngine;
using NaughtyAttributes;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Transform winGameUI, inGameUI;

    [Button]
    public void OpenWinGameUI() => winGameUI.gameObject.SetActive(true);
    [Button]
    public void CloseWinGameUI() => winGameUI.gameObject.SetActive(false);
    [Button]
    public void OpenInGameUI() => inGameUI.gameObject.SetActive(true);
    [Button]
    public void CloseInGameUI() => inGameUI.gameObject.SetActive(false);

}
