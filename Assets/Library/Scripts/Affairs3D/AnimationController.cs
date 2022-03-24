using UnityEngine;
using NaughtyAttributes;

namespace Affairs3D
{
    public class AnimationController : Singleton<AnimationController>
    {
        [SerializeField] private Animator animator;

        public void ActivatePose(string poseType)
        {
            switch (poseType)
            {
                case StringData.POSE_DOWN: ActivatePose0(); break;
                case StringData.POSE_RIGHT: ActivatePose1(); break;
                case StringData.POSE_LEFT: ActivatePose2(); break;
                case StringData.POSE_UP: ActivatePose3(); break;
                default: break;
            }
        }

        [Button]
        private void ActivatePose0()
        {
            animator.SetTrigger(StringData.POSE_DOWN);
        }
        [Button]
        private void ActivatePose1()
        {
            animator.SetTrigger(StringData.POSE_RIGHT);
        }
        [Button]
        private void ActivatePose2()
        {
            animator.SetTrigger(StringData.POSE_LEFT);
        }
        [Button]
        private void ActivatePose3()
        {
            animator.SetTrigger(StringData.POSE_UP);
        }


    }
}
