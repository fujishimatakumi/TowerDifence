using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLode : MonoBehaviour
{
    [SerializeField] Color m_color;
    [SerializeField] float m_multiplier;
    public void FadeScene(string sceneName)
    {
        Initiate.Fade(sceneName, m_color, m_multiplier);
    }
}
