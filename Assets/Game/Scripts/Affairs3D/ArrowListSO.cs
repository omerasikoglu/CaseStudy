using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

namespace Affairs3D
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ArrowList")]
    public class ArrowListSO : ScriptableObject
    {
        [SerializeField] private List<ArrowSO> arrowList;

        public List<ArrowSO> ArrowList { get { return arrowList; } }

    }
}
