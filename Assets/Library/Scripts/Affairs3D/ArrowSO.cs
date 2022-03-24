using UnityEngine;
using NaughtyAttributes;

namespace Affairs3D
{
    public enum SwipeType
    {
        Up, Down, Left, Right,
    }

    [CreateAssetMenu(menuName = "Scriptable Objects/Arrow")]
    public class ArrowSO : ScriptableObject
    {
        [SerializeField, ShowAssetPreview] private Sprite arrowSprite;
        [SerializeField, ShowAssetPreview] private Sprite poseSprite;
        [SerializeField] private SwipeType swipeType;

        public Sprite ArrowSprite { get { return arrowSprite; } }
        public Sprite PoseSprite { get { return poseSprite; } }
        public SwipeType SwipeType { get { return swipeType; } }
        //public string PoseType
        //{
        //    get
        //    {
        //        return SwipeType switch
        //        {
        //            SwipeType.Down => StringData.POSE_0,
        //            SwipeType.Right => StringData.POSE_1,
        //            SwipeType.Left => StringData.POSE_2,
        //            SwipeType.Up => StringData.POSE_3,
        //        };
        //    }
        //}
    }
}