using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using DG.Tweening;
using System;

namespace SummerBuster
{
    public enum RingColor
    {
        Yellow, Green, Blue, Pink,
    }
    public enum Side
    {
        Left, Middle, Right,
    }
    public class RingController : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> firstRingList, secondRingList, thirdRingList,
            firstClickedList, currentList;

        [SerializeField] private float firstViewportPosX;

        [SerializeField] private Side firstClickedSide, currentSide;

        private float maxHeight = 11.25f, minHeight = 1.75f, heightModifier = 1.75f;
        //width <3 ise 1. | 3-9 arasý ise 2. | >9 ise 3.

        private void OnMouseDown()
        {
            SetFirstClickSide();
        }
        private void OnMouseDrag()
        {
            //bastýðýn listede halka yoksa dön
            if (firstClickedList.Count == 0) return;

            firstClickedList[firstClickedList.Count - 1].DOMoveY(maxHeight, 1f);

            //current side deðiþirse horizontal hareket eder
            if (GetCurrentSide() == currentSide) return;

            UpdateCurrentSide();


        }
        private void OnMouseUp()
        {
            //düþme
            if (firstClickedList.Count == 0) return;

            //en tepedeki oynar
            firstClickedList[firstClickedList.Count - 1].
              DOMoveY(heightModifier * currentList.Count + 1, 1f).
              SetEase(Ease.OutBounce, 0.2f);

            //telefondan elimizi kaldýrdýðýmýzda hareket ettirmiþ miyiz
            if (firstClickedSide == currentSide) return;

            GetCurrentList().Add(firstClickedList[firstClickedList.Count - 1]);
            firstClickedList.RemoveAt(firstClickedList.Count - 1);


        }
        private void UpdateCurrentSide()
        {
            currentSide = GetCurrentSide();
            UpdateHorizontalPos(currentSide);
            currentList = GetCurrentList();
            //TODO: invisible ringler gözükür
        }



        private void SetFirstClickSide()
        {
            firstViewportPosX = UtilsClass.GetScreenToViewportPoint().x;

            if (firstViewportPosX <= 0.3) firstClickedSide = Side.Left;
            else if (firstViewportPosX >= 0.7f) firstClickedSide = Side.Right;
            else firstClickedSide = Side.Middle;

            currentSide = firstClickedSide;

            SetCurrentList();
        }
        private void SetCurrentList()
        {
            firstClickedList = firstClickedSide switch
            {
                Side.Left => firstRingList,
                Side.Middle => secondRingList,
                Side.Right => thirdRingList,
                _ => firstRingList
            };
            currentList = firstClickedList;
        }
        private Side GetCurrentSide()
        {
            float posX = UtilsClass.GetScreenToViewportPoint().x;

            if (posX <= 0.3) return Side.Left;
            else if (posX >= 0.7f) return Side.Right;
            else return Side.Middle;

        }
        private List<Transform> GetCurrentList()
        {
            float posX = UtilsClass.GetScreenToViewportPoint().x;

            if (posX <= 0.3) return firstRingList;
            else if (posX >= 0.7f) return thirdRingList;
            else return secondRingList;
        }


        private void UpdateHorizontalPos(Side side)
        {
            float posX = side switch
            {
                Side.Left => 0,
                Side.Middle => 6,
                Side.Right => 12,
                _ => 0
            };
            firstClickedList[firstClickedList.Count - 1].
                    DOMoveX(posX, 0.5f).
                    SetEase(Ease.InOutSine, 0.2f);
        }
        private void ShowInvisibleRings()
        {

        }
    }
}
