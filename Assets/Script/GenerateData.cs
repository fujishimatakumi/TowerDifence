using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateData : MonoBehaviour
{
   [field:SerializeField]
   public float GenerateMargin { get; set; }

   [field: SerializeField]
   public int GenerateNum { get; set; }

   [field: SerializeField]
   public float NextWateTime { get; set; }

   [field: SerializeField]
   public WayPoint Waypoint { get; set; }

    public bool NextFlug { get; set; }
    [field:SerializeField]
    public int GeneraterIndex { get; set; }
}
