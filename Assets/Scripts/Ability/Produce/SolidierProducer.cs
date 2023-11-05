using Assets.Scripts.CommonEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Ability
{
    internal class SolidierProducer : IProducer
    {
        List<ObjectType> Producers;
        public List<ObjectType> GetProducts()
        {
            return Producers;
        }
        public SolidierProducer
            ()
        { 

            Producers = new List<ObjectType> ();
            Producers.Add(ObjectType.Solidier1);
            Producers.Add(ObjectType.Solidier2);
            Producers.Add(ObjectType.Solidier3);
        }
    }
}
