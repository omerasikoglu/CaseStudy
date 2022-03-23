using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using DG.Tweening;
using System;

namespace SummerBuster
{
    public class RingController : MonoBehaviour
    {
        private enum Side
        {
            Left, Middle, Right,
        }

        [SerializeField] private List<Transform> firstRingList, secondRingList, thirdRingList;

        private List<Transform> firstClickedList, currentList;
        private Side firstClickedSide, currentSide;

        private float maxHeight = 11.25f, heightModifier = 1.75f;

        private void OnMouseDown()
        {
            SetFirstClickSide();
            //Debug.Log(ringColorDictionary[firstRingList[0]]);
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

            //ilk seçtiðimiz listedeki yuzugu býrakýrýz
            firstClickedList[firstClickedList.Count - 1].
              DOMoveY(heightModifier * currentList.Count + 1, 1f).
              SetEase(Ease.OutBounce, 0.2f);

            //telefondan elimizi kaldýrdýðýmýzda hareket ettirmiþ miyiz
            if (firstClickedSide == currentSide) return;

            GetCurrentList().Add(firstClickedList[firstClickedList.Count - 1]);
            firstClickedList.RemoveAt(firstClickedList.Count - 1);
            CheckAreYouWin();
        }
        private void SetFirstClickSide()
        {
            float firstViewportPosX = UtilsClass.GetScreenToViewportPoint().x;

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
        private void UpdateCurrentSide()
        {
            currentSide = GetCurrentSide();
            UpdateHorizontalPos(currentSide);
            currentList = GetCurrentList();
            ShowInvisibleRings();
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
        private void CheckAreYouWin()
        {
            Colors color = firstRingList[0].GetComponent<EldenRing>().GetColor();
            foreach (var item in firstRingList)
            {
                if (item.GetComponent<EldenRing>().GetColor() != color) break;
            }
            color = secondRingList[0].GetComponent<EldenRing>().GetColor();
            foreach (var item in secondRingList)
            {
                if (item.GetComponent<EldenRing>().GetColor() != color) break;
            }
            color = thirdRingList[0].GetComponent<EldenRing>().GetColor();
            foreach (var item in thirdRingList)
            {
                if (item.GetComponent<EldenRing>().GetColor() != color) break;
            }
            Debug.Log("win");

            //TODO: Win UI açýlýr
            GameManager.Instance.ChangeState(GameState.Win);
        }
    }
}
