using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

namespace Affairs3D
{
    public class ArrowSheet : MonoBehaviour
    {
        [SerializeField] private Transform youWinUI;
        private float sheetLength = 25f;

        private void Awake()
        {
            youWinUI.gameObject.SetActive(false);
            transform.DOLocalMoveX(-10000, 30f).SetEase(Ease.InSine);
        }
        private void Update()
        {
            if (transform.position.x <= -sheetLength) youWinUI.gameObject.SetActive(true);
        }
    }
}
