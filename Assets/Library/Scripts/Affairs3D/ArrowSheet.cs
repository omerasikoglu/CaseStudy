using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

namespace Affairs3D
{
    public class ArrowSheet : MonoBehaviour
    {
        private void Awake()
        {
            transform.DOLocalMoveX(-10000, 30f).SetEase(Ease.InSine);
        }
    }
}
