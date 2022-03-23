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

        [SerializeField, BoxGroup("Ring Lists")]
        private List<Transform> firstRingList, secondRingList, thirdRingList, ghostRingList;

        [SerializeField] private List<Transform> chibiList;

        private Side firstClickedSide, currentSide;

        private List<Transform> firstClickedList, currentList;

        private float maxHeight = 11.25f, heightModifier = 1.75f;

        //UNITY FUNCTIONS
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
            HideInvisibleRings();

            //telefondan elimizi kaldýrdýðýmýzda hareket ettirmiþ miyiz
            if (firstClickedSide == currentSide)
            {
                //ilk seçtiðimiz listedeki yuzugu býrakýrýz
                firstClickedList[firstClickedList.Count - 1].
                  DOMoveY(heightModifier * currentList.Count, 1f).
                  SetEase(Ease.OutBounce, 0.2f);
            }
            else
            {
                firstClickedList[firstClickedList.Count - 1].
                 DOMoveY(heightModifier * (currentList.Count + 1), 1f).
                 SetEase(Ease.OutBounce, 0.2f);

                //side deðiþirse listeler güncellenir
                GetCurrentList().Add(firstClickedList[firstClickedList.Count - 1]);
                firstClickedList.RemoveAt(firstClickedList.Count - 1);
                CheckAreYouWin();
            }


        }

        //SET
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

        //GET
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
        private Transform GetGhostRing()
        {
            return firstClickedList[firstClickedList.Count - 1].GetComponent<Ring>().GetColor() switch
            {
                Colors.blue => ghostRingList[0],
                Colors.green => ghostRingList[1],
                Colors.yellow => ghostRingList[2],
                _ => ghostRingList[0],
            };
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
            //tuttuðumuz halkanýn rengi yerleþtirceðimiz halkanýn rengiyle ayný deðilse gösterme
            //if (firstClickedList[firstClickedList.Count - 1].GetComponent<EldenRing>().GetColor() !=
            //    currentList[currentList.Count - 1].GetComponent<EldenRing>().GetColor()) return;

            float ringXPos = GetCurrentSide() switch
            {
                Side.Left => 0f,
                Side.Middle => 6f,
                Side.Right => 12f,
                _ => 0f,
            };
            GetGhostRing().gameObject.SetActive(true);
            GetGhostRing().position = new Vector3(ringXPos, heightModifier * (currentList.Count + 1), -5f);
        }
        private void HideInvisibleRings()
        {
            GetGhostRing().gameObject.SetActive(false);
        }
        [Button]
        private void CheckAreYouWin()
        {
            bool areYouWin = true;

            if (firstRingList.Count == 0) return;
            else
            {
                Colors color = firstRingList[0].GetComponent<Ring>().GetColor();
                for (int i = 0; i < firstRingList.Count; i++)
                {
                    if (firstRingList[i].GetComponent<Ring>().GetColor() != color)
                    {
                        areYouWin = false;
                    }
                }
            }
            if (secondRingList.Count == 0) return;
            else
            {
                Colors color = secondRingList[0].GetComponent<Ring>().GetColor();

                for (int i = 0; i < secondRingList.Count; i++)
                {
                    if (secondRingList[i].GetComponent<Ring>().GetColor() != color)
                    {
                        areYouWin = false;
                    }
                }
            }
            if (thirdRingList.Count == 0) return;
            else
            {
                Colors color = thirdRingList[0].GetComponent<Ring>().GetColor();

                for (int i = 0; i < thirdRingList.Count; i++)
                {
                    if (thirdRingList[i].GetComponent<Ring>().GetColor() != color)
                    {
                        areYouWin = false;
                    }
                }
            }

            if (areYouWin)
            {
                JumpingRings();
                JumpingChibis();


                Debug.Log("win");
                GameManager.Instance.ChangeState(GameState.Win);


            }
        }
        private void JumpingChibis()
        {
            foreach (var item in chibiList)
            {
                item.DOLocalMoveY(1, 0.2f).SetLoops(-1, LoopType.Incremental);
            }
        }
        private void JumpingRings()
        {
            //DOTween'ler hata veriyor 2 tanesi çakýþtýðý için o yüzden bu var
            currentList[currentList.Count - 1].DOMoveY(heightModifier * (currentList.Count), 0f);

            //jump for everyone
            foreach (var item in firstRingList)
            {
                item.DOLocalMoveY(10f, 1f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
            }
            foreach (var item in secondRingList)
            {
                item.DOLocalMoveY(10f, 1f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
            }
            foreach (var item in thirdRingList)
            {
                item.DOLocalMoveY(10f, 1f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
            }
        }
    }
}
