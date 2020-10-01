using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundGenerateData : MonoBehaviour
{
    public GenerateData[] m_generatDatas;
    [field:SerializeField]
    public float NextWaiteTime { get; private set; }
}
