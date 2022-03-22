using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using DG.Tweening;

namespace SummerBuster
{
    public enum RingColor
    {
        Yellow, Green, Blue, Pink,
    }
    public class RingController : MonoBehaviour
    {
        [SerializeField] private List<Transform> ringList;

        private int ringAmount;

        private float heightModifier = 1.75f;
        private float maxHeight = 9.5f, minHeight = 1.75f;

        private void Update()
        {
            if (ringList.Count == 0) return;

            if (Input.GetMouseButtonUp(0))
            {
                if (transform.position.y == minHeight) return;

                //en tepedeki oynar
                ringList[ringList.Count - 1].DOMoveY(heightModifier * ringList.Count, 1f).
                    SetEase(Ease.OutBounce, 0.2f)
                    ;
            }
            if (Input.GetMouseButton(0))
            {
                if (transform.position.y == maxHeight) return;

                ringList[ringList.Count - 1].DOMoveY(maxHeight, 1f)
                    ;
            }
        }
    }
}
