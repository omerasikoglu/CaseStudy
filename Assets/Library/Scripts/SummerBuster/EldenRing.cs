using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;

//Solo Ring Script

namespace SummerBuster
{
    public enum Colors
    {
        blue, green, yellow,
    };
    public class EldenRing : MonoBehaviour
    {
        [SerializeField, BoxGroup("[Only One Bool Stands]")]
        private bool blue, green, yellow;

        private Dictionary<Colors, bool> newDic, prevDic;

        #region One Bool Stands - Functions
        private void OnValidate() => ValidateOneBoolStands();
        private void ValidateOneBoolStands()
        {
            //yeni degerler guncellenir
            RefreshNewDic();

            //prev dic null'sa
            CheckPrevDicIsNull();

            //1den fazla bool'u varsa bi daha guncellenir
            CheckTrueCount();
        }
        private void RefreshNewDic()
        {
            newDic = new Dictionary<Colors, bool>()
        {
            { Colors.blue, blue },
            { Colors.green, green },
            { Colors.yellow, yellow },
        };
        }
        private void CheckPrevDicIsNull()
        {
            //eski liste ilk defa olusuyorsa
            prevDic ??= newDic;
        }
        private void CheckTrueCount()
        {
            int totalTrue = newDic.Where((t, i) => newDic[(Colors)i].Equals(true)).Count();

            if (totalTrue < 1)
            {
                blue = true;
                RefreshNewDic();
                prevDic = newDic;
            }
            else if (totalTrue > 1)
            {
                //son true olan bool disindakiler false olur
                for (int i = 0; i < newDic.Count; i++)
                {
                    if (prevDic[(Colors)i] || !newDic[(Colors)i]) continue;

                    SetBools((Colors)i);
                }
            }
            //totalTrue == 1 ise bi sey yapma devam
        }
        private void SetBools(Colors boolEnum)
        {
            switch (boolEnum)
            {
                case Colors.green:
                    green = true; blue = false; yellow = false; break;
                case Colors.blue:
                    green = false; blue = true; yellow = false; break;
                case Colors.yellow:
                    green = false; blue = false; yellow = true; break;
                default:
                    green = false; blue = true; yellow = false; break;
            }
            RefreshNewDic();
            prevDic = newDic;
        }
        #endregion

        public Colors GetColor()
        {
            if (green) return Colors.green;
            else if (blue) return Colors.blue;
            else return Colors.yellow;
        }
    }
}
