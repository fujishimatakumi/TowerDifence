using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WayPoint : MonoBehaviour
{
    //オブジェクトが移動するためのパス
    public Transform[] m_wayPoints;

    [SerializeField] string m_tagToTilemap = "setField";

    private void Start()
    {
        SetToTilemapCenterWorldPoint();
    }
    public void SetToTilemapCenterWorldPoint()
    {
        Tilemap tm = GameObject.FindGameObjectWithTag(m_tagToTilemap).GetComponent<Tilemap>();
        for (int i = 0; i < m_wayPoints.Length; i++)
        {
            var tilepos = tm.WorldToCell(m_wayPoints[i].position);
            Vector3 center = tm.GetCellCenterWorld(tilepos);
            m_wayPoints[i].position = center;
        }
    }
    
}
