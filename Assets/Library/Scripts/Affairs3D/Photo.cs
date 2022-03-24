using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Affairs3D
{
    public class Photo : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(UtilsClass.Wait(() =>
           {
               transform.GetComponent<Image>().DOColor(Color.white, 5f);
               transform.DOLocalRotate(new Vector3(0f, 0f, -15f), 6f).
               SetEase(Ease.InOutSine,6f).
               SetLoops(-1, LoopType.Yoyo);
           }, 5f));
        }
    }
}
