using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows.Markup;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
//見つからない場合無限ループに入るので修正が必要

public enum Directhon
{ 
    UP,
    DOWN,
    RIGHT,
    LEFT
}
public class Astar
{
    public int FieldSizeX { get; set; }
    public int FieldSizeY { get; set; }
    Node[,] m_node;
    List<Vector2Int> tmpList = new List<Vector2Int>();

    int m_hCost;



    public void Initiait(int sizeX, int sizeY)
    {
        FieldSizeX = sizeX;
        FieldSizeY = sizeY;

        m_node = new Node[sizeX, sizeY];

        SethCost(1);

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                m_node[x, y] = Node.CreateBrankNode(new Vector2Int(x, y));
            }
        }
    }
    private void SethCost(int cost)
    {
        m_hCost = cost;
    }

    public bool SerchRout(Vector2Int startNodePos, Vector2Int goleNodePos, List<Vector2Int> routResult)
    {
        Vector2Int[] dir = { new Vector2Int(0, -1),//up
                             new Vector2Int(0, 1), //down
                             new Vector2Int(1, 0),//right
                             new Vector2Int(-1, 0)//left
                            };
        if (startNodePos == goleNodePos)
        {
            return false;
        }

        while (true)
        {
            var bestScorePos = GetBestScorePos();

        }
        return false;
    }

    private bool CheckMapRange(Vector2Int pos)
    {
        if (pos.x < 0) return false;
        if (pos.y < 0) return false;
        if (pos.x >= FieldSizeX) return false;
        if (pos.y >= FieldSizeY) return false;
        if (m_node[pos.x, pos.y].m_status == Status.Start) return false;
        if (m_node[pos.x, pos.y].m_status == Status.Lock) return false;
        if (m_node[pos.x, pos.y].m_status == Status.Gole) return true;
        if (m_node[pos.x, pos.y].m_status == Status.None) return true;

        return false;
    }

    private Vector2Int GetBestScorePos()
    {
        var min = float.MaxValue;
        Vector2Int bestSocrePos = new Vector2Int(0, 0);
        for (int x = 0; x < FieldSizeX; x++)
        {
            for (int y = 0; y < FieldSizeY; y++)
            {
                if (m_node[x,y].m_status == Status.Open)
                {
                    if (min > m_node[x,y].GetScore())
                    {
                        min = m_node[x, y].GetScore();
                        bestSocrePos = m_node[x, y].m_nodePos;
                    }
                }
            }
        }
        return bestSocrePos;
    }
}
