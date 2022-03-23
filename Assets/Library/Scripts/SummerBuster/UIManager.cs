using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Transform winGameUI, inGameUI;
    [SerializeField] private TextMeshProUGUI levelTextMesh;
    [Button]
    public void OpenWinGameUI() => winGameUI.gameObject.SetActive(true);
    [Button]
    public void CloseWinGameUI() => winGameUI.gameObject.SetActive(false);
    [Button]
    public void OpenInGameUI() => inGameUI.gameObject.SetActive(true);
    [Button]
    public void CloseInGameUI() => inGameUI.gameObject.SetActive(false);

    public void UpdateLevelUI()
    {
        levelTextMesh.SetText($"Level {PlayerPrefs.GetInt(StringData.LEVEL).ToString()}");
    }
}
