using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows.Markup;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public int FieldSizeX { get; set; }
    public int FieldSizeY { get; set; }
    Node[,] m_node;
    Node[,] m_openNode;
    Node[,] m_closeNode;

    int m_hCost;

    

    public void Initiait(int sizeX, int sizeY)
    {
        FieldSizeX = sizeX;
        FieldSizeY = sizeY;

        m_node = new Node[sizeX, sizeY];
        m_openNode = new Node[sizeX, sizeY];
        m_closeNode = new Node[sizeX, sizeY];

        SethCost(1);

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                m_node[x, y] = Node.CreateBrankNode(new Vector2Int(x, y));
                m_openNode[x, y] = Node.CreateBrankNode(new Vector2Int(x, y));
                m_closeNode[x, y] = Node.CreateBrankNode(new Vector2Int(x, y));
            }
        }
    }

    public bool SerchRout(Vector2Int startNodePos, Vector2Int goleNodePos, List<Vector2Int> routResult)
    {
        ResetNode();
        if (startNodePos == goleNodePos)
        {
            return false;
        }

        for (int x = 0; x < FieldSizeX; x++)
        {
            for (int y = 0; y < FieldSizeY; y++)
            {
                //仮想コストの設定
                m_node[x, y].UpdateGoleNodePos(goleNodePos);
                m_openNode[x, y].UpdateGoleNodePos(goleNodePos);
                m_closeNode[x, y].UpdateGoleNodePos(goleNodePos);
            }
        }
        //スタート地点の初期化
        m_openNode[startNodePos.x, startNodePos.y] = Node.CreateNode(startNodePos, goleNodePos);
        m_openNode[startNodePos.x, startNodePos.y].SetFromNodePos(startNodePos);
        m_openNode[startNodePos.x, startNodePos.y].Add();
        
        while (true)
        {
            var bestScorePos = GetBestScorePos();
            OpenNode(bestScorePos, goleNodePos);
            if (bestScorePos == goleNodePos)
            {
                break;
            }
        }

        ResolveRoute(startNodePos, goleNodePos, routResult);

        return true;
    }

    private void SethCost(int cost)
    {
        m_hCost = cost;
    }

    public void ResetNode()
    {
        for (int x = 0; x < FieldSizeX; x++)
        {
            for (int y = 0; y < FieldSizeY; y++)
            {
                m_node[x, y].Clear();
                m_openNode[x, y].Clear();
                m_closeNode[x, y].Clear();
            }
        }
    }

    private void OpenNode(Vector2Int bestScorePos, Vector2Int golePos)
    {
        for (int bx = -1; bx < 2; bx++)
        {
            for (int by = -1; by < 2; by++)
            {
                int cx = bestScorePos.x + bx;
                int cy = bestScorePos.y + by;
                if (CheckOutofRange(bx, by, bestScorePos.x, bestScorePos.y) == false)
                {
                    continue;
                }
                if (m_node[cx, cy].m_isLock == true)
                {
                    continue;
                }

                var addCost = 1;
                m_node[cx, cy].SetCost(m_openNode[cx, cy].m_cost + addCost);
                m_node[cx, cy].SetFromNodePos(bestScorePos);

                UpdateNodeList(cx, cy, golePos);
            }
        }

        m_closeNode[bestScorePos.x, bestScorePos.y] = m_openNode[bestScorePos.x, bestScorePos.y];
        m_closeNode[bestScorePos.x, bestScorePos.y].Add();
        m_openNode[bestScorePos.x, bestScorePos.y].Remove();
    }

    private void UpdateNodeList(int x, int y, Vector2Int golePos)
    {
        if (m_openNode[x, y].m_isActiv)
        {
            //優秀なスコアなら追加
            if (m_openNode[x, y].GetScore() > m_node[x, y].GetScore())
            {
                m_openNode[x, y].SetCost(m_node[x, y].m_cost);
                m_openNode[x, y].SetFromNodePos(m_node[x, y].m_fromNodePos);
            }
        }
        else if (m_closeNode[x, y].m_isActiv)
        {
            //優秀なスコアならオープンリストに追加
            if (m_closeNode[x, y].GetScore() > m_node[x, y].GetScore())
            {
                m_closeNode[x, y].Remove();
                m_openNode[x, y].Add();
                m_openNode[x, y].SetCost(m_node[x, y].m_cost);
                m_openNode[x, y].SetFromNodePos(m_node[x, y].m_fromNodePos);
            }
        }
        else
        {
            //そうでなければ
            m_openNode[x, y] = new Node(new Vector2Int(x, y), golePos);
            m_openNode[x, y].SetFromNodePos(m_node[x, y].m_fromNodePos);
            m_openNode[x, y].SetCost(m_node[x, y].m_cost);
            m_openNode[x, y].Add();
        }
    }
    

    private bool CheckOutofRange(int bx, int by, int x, int y)
    {
        if (bx == 0 && by == 0 
            || bx == 1 && by == 1 
            || bx == -1 && by == -1 
            || bx == -1 && by == 1 
            || bx == 1 && by == -1) return false;

        int cx = bx + x;
        int cy = by + y;

        if (cx < 0 || cx == FieldSizeX || cy < 0 || cy == FieldSizeY) return false;


        return true;
    }

    private Vector2Int GetBestScorePos()
    {
        var value = new Vector2Int(0, 0);

        int min = int.MaxValue;
        for (int x = 0; x < FieldSizeX; x++)
        {
            for (int y = 0; y < FieldSizeY; y++)
            {
                if (!m_openNode[x,y].m_isActiv)
                {
                    continue;
                }

                if (min > m_openNode[x,y].GetScore())
                {
                    min = m_openNode[x, y].GetScore();
                    value = m_openNode[x, y].m_nodePos;
                }
            }
        }

        return value;
    }

    private void ResolveRoute(Vector2Int startNodePos, Vector2Int goleNodePos, List<Vector2Int> result)
    {
        if (result == null)
        {
            result = new List<Vector2Int>();
        }
        else
        {
            result.Clear();
        }

        var node = m_closeNode[goleNodePos.x, goleNodePos.y];
        result.Add(goleNodePos);

        int count = 0;
        int tryCount = 1000;
        bool isSucsess = false;

        while (count < tryCount)
        {
            var beforNode = result[0];
            if (beforNode == node.m_fromNodePos) { break; }//同じポジションなので失敗

            if (node.m_fromNodePos == startNodePos)
            {
                isSucsess = true;
                break;
            }
            else
            {
                result.Insert(0, node.m_fromNodePos);
            }

            node = m_closeNode[node.m_fromNodePos.x, node.m_fromNodePos.y];

            count++;
        }

        if (!isSucsess)
        {
            //失敗
        }
    }

    public void SetLock(Vector2Int lockNodePos, bool lockStatus)
    {
        m_node[lockNodePos.x, lockNodePos.y].SetIsLock(lockStatus);
    }
}
