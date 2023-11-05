using Assets.Scripts.CommonEnums;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
    List<Object> ObjectList;
    private int MaxX;
    private int MaxY;
    private Node[,] Nodes;

    public GridManager(int w, int h)
    {
        MaxX = w;
        MaxY = h;
        ObjectList = new List<Object>();
        Nodes = new Node[w, h];
        for(int i = 0; i < MaxX; i ++)
        {
            for(int j = 0; j < MaxY; j ++)
            {
                Nodes[i,j] = new Node();
                Nodes[i,j].position = new Vector2 (i,j);
            }
        }
    }
    public bool CheckBuildable(Object Obj)
    {
        if ((int)Obj.gameObject.transform.position.x + (Obj.Size - 1) >= MaxX || (int)Obj.gameObject.transform.position.x < 0)
            return false;
        if ((int)Obj.gameObject.transform.position.y >= MaxY || (int)Obj.gameObject.transform.position.y - (Obj.Size - 1) < 0)
            return false;

        foreach (Object o in ObjectList)
        {
            if (o.IsCollision(Obj))
                return false;
        }
        return true;
    }
    public bool AddObject(Object Obj)
    {
        if(CheckBuildable(Obj))
        {
            Obj.SpriteRenderer.color = Color.white;
            ObjectList.Add(Obj);
            Obj.MovementComponent.Init();
            return true;
        }
        return false;
    }
    public void RemoveObject(Object Item)
    {
        ObjectList.Remove(Item);
        Item.GetComponent<PooledPrefab>().Pool.ReturnObject(Item.gameObject);
    }
    public bool CheckLocationMovebility(Vector2 Loc)
    {
        if (!InGridControl(Loc))
            return false;
       foreach (Object o in ObjectList)
       {
            if(o.IsCollision(Loc))
            {
                return false;
            }
       }
        return true;
    }
    public bool InGridControl(Vector2 Loc)
    {
        if (Loc.x < 0 || Loc.x >= MaxX || Loc.y < 0 || Loc.y >= MaxY)
            return false;
        return true;
    }
    public Object GetObjectOnLocation(Vector2 Loc)
    {
        foreach (Object o in ObjectList)
        {
            if (o.IsCollision(Loc))
            {
                return o;
            }
        }
        return null;
    }
    public List<Node> FindPath(Object Source, Vector2 Target)
    {
        List<Node> list = new List<Node>();
        for (int i = 0; i < MaxX; i++)
        {
            for (int j = 0; j < MaxY; j++)
            {
                Nodes[i, j].isBlock = false;
                Nodes[i, j].Neigbours.Clear();
            }
        }
        foreach (Object o in ObjectList)
        {
            if (o == Source)
            {
                continue;
            }
            List<Vector2> Area = o.GetArea();
            foreach (Vector2 p in Area)
            {
                Nodes[(int)p.x,(int)p.y].isBlock = true;

            }
        }
        for (int i = 0; i < MaxX; i++)
        {
            for (int j = 0; j < MaxY; j++)
            {
                AddNeigbours(Nodes[i, j], i, j);

            }
        }
        Node Node = Nodes[(int)Source.transform.position.x, (int)Source.transform.position.y];
        list.Add(Node);
        while (true)
        {
            Node = FindBestChild(Node, Target);
            if (Node == null)
                break;
            else
                list.Add(Node);
            if(list.Count > (MaxX*MaxY))
            {
                Debug.Log("Path Not Found");
                return new List<Node> { Node };

            }
        }
        return list;
    }
    private void AddNeigbours(Node n, int x, int y)
    {
        if(x < 0 || x >= MaxX || y < 0 || y >= MaxY)
        {
            return;
        }
        if((x + 1) < MaxX && !Nodes[x + 1, y].isBlock)
        {
            n.Neigbours.Add(Nodes[x + 1, y]);
        }
        if ((x - 1) >= 0 && !Nodes[x - 1, y].isBlock)
        {
            n.Neigbours.Add(Nodes[x - 1, y]);
        }
        if ((y + 1) < MaxY && !Nodes[x, y + 1].isBlock)
        {
            n.Neigbours.Add(Nodes[x, y + 1]);
        }
        if ((y - 1) >= 0 && !Nodes[x, y - 1].isBlock)
        {
            n.Neigbours.Add(Nodes[x, y - 1]);
        }

    }
    private Node FindBestChild(Node n, Vector2 Target)
    {
        if (Vector2.Equals(n.position,Target))
            return null;
        Node Selected = null;
        foreach(Node n2 in n.Neigbours)
        {
            if(Selected == null)
            {
                Selected = n2;
                continue;
            }
            else
            {
                if(Vector2.Distance(Selected.position,Target) > Vector2.Distance(n2.position, Target))
                {
                    Selected = n2;
                }
            }

        }
        return Selected;
    }
    public bool FindNearLocation(Object Target, ref Vector3 Result)
    {
        int x = (int)Target.transform.position.x;
        int y = (int)Target.transform.position.y;
        int Count = Target.Size + 2;

        for(int i = 0; i < Count; i++)
        {
            for(int j = 0; j < Count; j ++)
            {
                Vector2 Temp = new Vector2 (x+(i-1),y +(j - 2));
                if(CheckLocationMovebility(Temp))
                {
                    Result = Temp;
                    return true;
                }
            }
        }
        return false;
    }
    public bool FindNearLocation(Vector3 Target, ref Vector3 Result)
    {
        int x = (int)Target.x;
        int y = (int)Target.y;

        Vector2 Temp = new Vector2(x , y);
        if (CheckLocationMovebility(Temp))
        {
            Result = Temp;
            return true;
        }
        return false;
    }
    public bool FindRandomEmptyLocation(ref Vector3 Result)
    {
        for (int i = 0; i < MaxX; i++)
        {
            for (int j = 0; j < MaxY; j++)
            {
                Vector2 Temp = new Vector2(i,j);
                if (CheckLocationMovebility(Temp))
                {
                    Result = Temp;
                    return true;
                }
            }
        }
        return false;
    }

}
