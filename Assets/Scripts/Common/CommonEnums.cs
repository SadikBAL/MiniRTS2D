using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CommonEnums
{
    public enum ObjectType
    {
        Solidier1,
        Solidier2,
        Solidier3,
        Barracks,
        PowerPlant
    }
    public enum GameState
    {
        WaitInput,
        BuildMode,
        Select,
    }
    public class Node
    {
        public bool isBlock;
        public Vector2 position;
        public List<Node> Neigbours;
        public Node() 
        {
            Neigbours = new List<Node>();
        }
    }
}
