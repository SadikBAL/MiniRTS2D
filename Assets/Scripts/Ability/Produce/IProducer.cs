using Assets.Scripts.CommonEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Ability
{
    public interface IProducer
    {
        public List<ObjectType> GetProducts();
    }
}
