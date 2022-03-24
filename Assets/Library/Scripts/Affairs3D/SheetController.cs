using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.UI;

//gelecek komutlarýn sheet'i tutar
namespace Affairs3D
{

    public class SheetController : MonoBehaviour
    {
        [SerializeField] private ArrowListSO arrowListSO;

        [SerializeField] private Transform posePosition;

        [SerializeField] private List<Transform> sheetArrowPosList;

        private Vector2 firstInputPos, lastInputPos;

        private List<SwipeType> swipeList;
        private SwipeType currentSwipe;

        //yeþilde mi, kaydýrma aktif mi
        [SerializeField] private bool isSwipeTime = false;

        private void Awake()
        {
            swipeList = new List<SwipeType>(5) { SwipeType.Up, SwipeType.Up, SwipeType.Up, SwipeType.Up, SwipeType.Up };
            GenerateRandomArrowsToSheet();
        }
        private void Update()
        {
            CheckIsSwipeTime();
        }

        private void CheckIsSwipeTime()
        {
            if (sheetArrowPosList[0].position.x <= 4.6f && sheetArrowPosList[0].position.x >= 3.0f)
            {
                isSwipeTime = true; currentSwipe = swipeList[0];
            }
            else if (sheetArrowPosList[1].position.x <= 4.6f && sheetArrowPosList[1].position.x >= 3.0f)
            {
                isSwipeTime = true; currentSwipe = swipeList[1];
            }
            else if (sheetArrowPosList[2].position.x <= 4.6f && sheetArrowPosList[2].position.x >= 3.0f)
            {
                isSwipeTime = true; currentSwipe = swipeList[2];
            }
            else if (sheetArrowPosList[3].position.x <= 4.6f && sheetArrowPosList[3].position.x >= 3.0f)
            {
                isSwipeTime = true; currentSwipe = swipeList[3];
            }
            else if (sheetArrowPosList[4].position.x <= 4.6f && sheetArrowPosList[4].position.x >= 3.0f)
            {
                isSwipeTime = true; currentSwipe = swipeList[4];
            }
            else isSwipeTime = false;
        }

        private void OnMouseDown()
        {
            firstInputPos = (Vector2)Input.mousePosition;
        }

        private void OnMouseUp()
        {
            //Debug.Log(lastInputPos.x + " " + firstInputPos.x + " " + currentSwipe);

            SwipeHandler();
        }

        private void SwipeHandler()
        {
            //zamanýnda basmadýysa yok say
            if (!isSwipeTime) return;

            lastInputPos = (Vector2)Input.mousePosition;

            //check horizontal
            if (lastInputPos.x > firstInputPos.x)
            {

                //saða kaydý
                if (currentSwipe == SwipeType.Right)
                {
                    AnimationController.Instance.ActivatePose(StringData.POSE_UP);
                    Debug.Log("Right");
                }
            }
            else if (lastInputPos.x < firstInputPos.x)
            {

                //sola kaydý
                if (currentSwipe == SwipeType.Left)
                {
                    AnimationController.Instance.ActivatePose(StringData.POSE_LEFT);
                    Debug.Log("Left");
                }
            }

            //check vertical
            if (lastInputPos.y < firstInputPos.y)
            {

                //aþaðý
                if (currentSwipe == SwipeType.Down)
                {
                    AnimationController.Instance.ActivatePose(StringData.POSE_DOWN);
                    Debug.Log("Down");
                }
            }
            else if (lastInputPos.y > firstInputPos.y)
            {
                //yukarý
                if (currentSwipe == SwipeType.Up)
                {
                    AnimationController.Instance.ActivatePose(StringData.POSE_UP);
                    Debug.Log("Up");
                }
            }
        }

        private void CheckIsItTrueSwipe()
        {

        }

        [Button]
        private void GenerateRandomArrowsToSheet()
        {
            for (int i = 0; i < sheetArrowPosList.Count; i++)
            {
                ArrowSO arrow = GenerateOneRandomArrow();
                sheetArrowPosList[i].transform.GetComponent<Image>().sprite = arrow.ArrowSprite;
                swipeList[i] = arrow.SwipeType;
            }

        }

        private ArrowSO GenerateOneRandomArrow()
        {
            float i = UnityEngine.Random.value;

            if (i < 0.25f)
            {
                return arrowListSO.ArrowList[0];
            }
            else if (i >= 0.25f && i < 0.5f)
            {
                return arrowListSO.ArrowList[1];
            }
            else if (i >= 0.5f && i < 0.75f)
            {
                return arrowListSO.ArrowList[2];
            }
            else
            {
                return arrowListSO.ArrowList[3];
            }
        }


    }

}