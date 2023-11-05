using Assets.Scripts.CommonEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "CommonDatas/Data", order = 1)]
    public class CommonDatas : ScriptableObject
    {
        public List<Sprite> Icons;
        public List<ObjectType> Types;
    }
}
