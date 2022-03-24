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
        [SerializeField] private List<Transform> sheetArrowPosList;
        [SerializeField] private Image posePosition; //fotoðraflarýn duracaðý konum

        private Vector2 firstInputPos, lastInputPos;

        private List<SwipeType> swipeList;
        private SwipeType currentSwipe;

        private bool isSwipeTime = false; //yeþilde mi, kaydýrma aktif mi

        private void Awake()
        {
            InitLists();
            GenerateRandomArrowsToSheet();
        }

        private void Update()
        {
            CheckIsSwipeTime();
        }
        private void OnMouseDown()
        {
            firstInputPos = (Vector2)Input.mousePosition;
        }
        private void OnMouseUp()
        {
            SwipeHandler();
        }

        private void CheckIsSwipeTime()
        {
            //arrow'lar kutucuðun içinde mi ekranda, observer'la yazsam daha güzel olurdu
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
            else if (sheetArrowPosList[5].position.x <= 4.6f && sheetArrowPosList[5].position.x >= 3.0f)
            {
                isSwipeTime = true; currentSwipe = swipeList[5];
            }
            else if (sheetArrowPosList[6].position.x <= 4.6f && sheetArrowPosList[6].position.x >= 3.0f)
            {
                isSwipeTime = true; currentSwipe = swipeList[6];
            }
            else isSwipeTime = false;
        }
        private void SwipeHandler()
        {
            //zamanýnda basmadýysa yok say
            if (!isSwipeTime) return;

            lastInputPos = (Vector2)Input.mousePosition;

            //Check horizontal
            if (lastInputPos.x > firstInputPos.x && currentSwipe == SwipeType.Right)
            {
                //saða kaydý
                AnimationController.Instance.ActivatePose(StringData.POSE_RIGHT);
            }
            else if (lastInputPos.x < firstInputPos.x && currentSwipe == SwipeType.Left)
            {
                //sola kaydý
                AnimationController.Instance.ActivatePose(StringData.POSE_LEFT);
            }

            //Check vertical
            if (lastInputPos.y < firstInputPos.y && currentSwipe == SwipeType.Down)
            {
                //aþaðý
                AnimationController.Instance.ActivatePose(StringData.POSE_DOWN);

            }
            else if (lastInputPos.y > firstInputPos.y && currentSwipe == SwipeType.Up)
            {
                //yukarý
                AnimationController.Instance.ActivatePose(StringData.POSE_UP);
            }
            posePosition.sprite = GetSpriteFromSwipeType(currentSwipe);
        }
        private void CheckIsItTrueSwipe()
        {
            //TODO: Fail FX
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
        private void InitLists()
        {
            swipeList = new List<SwipeType>(7) { SwipeType.Up, SwipeType.Up, SwipeType.Up, SwipeType.Up, SwipeType.Up, SwipeType.Up, SwipeType.Up };
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

        private Sprite GetSpriteFromSwipeType(SwipeType swipeType)
        {
            return swipeType switch
            {
                SwipeType.Left => arrowListSO.ArrowList[0].PoseSprite,
                SwipeType.Right => arrowListSO.ArrowList[1].PoseSprite,
                SwipeType.Up => arrowListSO.ArrowList[2].PoseSprite,
                SwipeType.Down => arrowListSO.ArrowList[3].PoseSprite,
                _ => arrowListSO.ArrowList[0].PoseSprite,
            };
        }
    }

}