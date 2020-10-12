using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Status
{
    None,
    Open,
    Closed
}
public class Node : MonoBehaviour
{
    public Vector2Int m_nodePos { get; }
    public Vector2Int m_fromNodePos { get; private set; }
    public int m_cost { get; private set; }
    //仮想コスト
    public int m_hCost;
    public bool m_isLock { get; set; }
    public bool m_isActiv { get; set; }

    public static Node CreateBrankNode(Vector2Int position)
    {
        return new Node(position,new Vector2Int(-1,-1));
    }

    public static Node CreateNode(Vector2Int pos, Vector2Int golePos)
    {
        return new Node(pos, golePos);
    }

    public  void UpdateGoleNodePos(Vector2Int golePos)
    {
        m_hCost = (golePos.x - m_nodePos.x) + (golePos.y - m_nodePos.y);
    }


    public  Node(Vector2Int nodePos, Vector2Int golenodePos)
    {
        m_nodePos = nodePos;
        m_isLock = false;
        Remove();
        m_cost = 0;
        UpdateGoleNodePos(golenodePos);
    }

    public int GetScore()
    {
        return m_cost + m_hCost;
    }

    public void SetFromNodePos(Vector2Int pos)
    {
        m_fromNodePos = pos;
    }
    public void Remove()
    {
        m_isActiv = false;
    }

    public void Add()
    {
        m_isActiv = true;
    }

    public void SetIsLock(bool isLock)
    {
        m_isLock = isLock;
    }

    public void SetCost(int cost)
    {
        m_cost = cost;
    }

    public void Clear()
    {
        Remove();
        m_cost = 0;
        UpdateGoleNodePos(new Vector2Int(-1, -1));
    }
}
