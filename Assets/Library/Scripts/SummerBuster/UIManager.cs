using UnityEngine;
using NaughtyAttributes;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Transform winGameUI;

    [Button]
    public void OpenWinGameUI() => winGameUI.gameObject.SetActive(true);
    [Button]
    public void CloseWinGameUI() => winGameUI.gameObject.SetActive(false);


}
