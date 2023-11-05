using Assets.Scripts.CommonEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Ability
{
    public class NoneProducer : IProducer
    {
        public List<ObjectType> GetProducts()
        {
            return new List<ObjectType>();
        }
    }
}
