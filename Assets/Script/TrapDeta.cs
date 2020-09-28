﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TrapDeta : MonoBehaviour
{
    [field: SerializeField]
    public int m_cost { get; private set; }
    [field:SerializeField]
    public int m_returnCost { get; private set; }
    [SerializeField] GameObject m_canvas;

    public void EnableCanvas()
    {
        m_canvas.SetActive(true);
    }

    public void DesableCanvas()
    {
        m_canvas.SetActive(false);
    }

    public void DestroyTrap()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        gm.AddResourcePoint(m_returnCost);
        Destroy(this.gameObject);
    }
}
