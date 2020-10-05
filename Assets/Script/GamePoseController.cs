using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePoseController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_pouseUI;
    SceneLode m_sceneLode;
    private void Update()
    {
        Pouse();
    }
    // Update is called once per frame
    public void Pouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_pouseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ReturnStage(string sceneName)
    {
        GeneraterController gc = GameObject.FindGameObjectWithTag("GContr").GetComponent<GeneraterController>();
        gc.StopGenerate();
        Time.timeScale = 1;
        m_sceneLode = GetComponent<SceneLode>();
        m_sceneLode.FadeScene(sceneName);
    }

    public void Restart()
    {
        m_pouseUI.SetActive(false);
        Time.timeScale = 1;
    }
}
