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
    public float m_cost { get; private set; }
    //仮想コスト
    public float m_hCost;
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
        m_hCost = Mathf.Sqrt(Mathf.Pow((golePos.x - m_nodePos.x),2) + Mathf.Pow((golePos.y - m_nodePos.y),2));
    }


    public  Node(Vector2Int nodePos, Vector2Int golenodePos)
    {
        m_nodePos = nodePos;
        m_isLock = false;
        Remove();
        m_cost = 0;
        UpdateGoleNodePos(golenodePos);
    }

    public float GetScore()
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

    public void SetCost(float cost)
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
