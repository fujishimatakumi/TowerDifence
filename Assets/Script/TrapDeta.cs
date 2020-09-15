using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TrapDeta : MonoBehaviour
{
    [field: SerializeField]
    public int m_cost { get; private set; }
    [field:SerializeField]
    public int m_returnCost { get; private set; }
}
