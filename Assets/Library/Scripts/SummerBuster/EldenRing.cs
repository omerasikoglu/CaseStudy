using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace SummerBuster
{
    public class EldenRing : MonoBehaviour
    {
        private float screenPosZ;
        private Vector3 offset;
        private Transform startingPosition => transform;
        private float maxHeight = 9.5f, minHeight = 1.75f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (transform.position.y == minHeight) return;

                transform.DOMoveY(minHeight, 1f).
                    SetEase(Ease.OutBounce, 0.2f)
                    ;
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (transform.position.y == maxHeight) return;

                transform.DOMoveY(maxHeight, 1f)
                    ;
            }
            //objenin yeni pozisyonu = dünyadaki mouse pozisyonu + offset
            //transform.position = UtilsClass.GetScreenToWorldPoint(posZ: screenPosZ) + offset;
        }
        //private void OnMouseDown()
        //{
        //    Debug.Log("clicked");
        //    var position = gameObject.transform.position;
        //    screenPosZ = UtilsClass.GetWorldToScreenPoint(position).z;
        //    offset = position - UtilsClass.GetScreenToWorldPoint(posZ: screenPosZ);
        //}
        //private void OnMouseDrag()
        //{
        //    transform.position = UtilsClass.GetScreenToWorldPoint(posZ: screenPosZ) + offset;
        //}
    }
}
