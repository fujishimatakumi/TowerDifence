using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// ゲームの進行状態を表すenum
/// </summary>
public enum GameStatus
{
    /// <summary>初期化</summary>
    initiate,
    /// <summary>ゲームスタート</summary>
    gameStart,
    /// <summary>ゲーム中</summary>
    nowGame,
    /// <summary>ゲームクリア</summary>
    Clear,
    /// <summary>ゲームオーバー</summary>
    GameOver,
    GameEnd,
    /// <summary>デバッグ用</summary>
    Debug
}
public class GameManager : MonoBehaviour
{
    public GameStatus m_status { get; set; }
    [SerializeField] int m_rimit = 3;
    [field:SerializeField]
    public int EnemyNum { get; set; }
    //UI用各種テキスト
    [SerializeField] Text m_enemyNumText;
    [SerializeField] Text m_towerHPText;
    [SerializeField] Text m_resourceText;
    //アイテムを購入するのに必要なポイント

    [field:SerializeField]
    public int m_resourcePoint { get; private set; } = 1200;
    //リソースポイントを表示するテキスト
    
    [SerializeField] GameObject m_clearImage;
    float alpha = 0;
    [SerializeField] float alphaMaguni = 0.01f;
    AudioSource m_audio;
    // Start is called before the first frame update
    void Start()
    {
        m_status = GameStatus.initiate;
        m_audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        SetEnemyNum();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_status == GameStatus.Debug) return;
        
        switch (m_status)
        {
            case GameStatus.initiate:
                m_resourceText.text = m_resourcePoint.ToString();
                m_enemyNumText.text = EnemyNum.ToString();
                GetTowerHP();
                m_status = GameStatus.gameStart;
                break;
            case GameStatus.gameStart:
                m_status = GameStatus.nowGame;
                break;
            case GameStatus.nowGame:
                break;
            case GameStatus.Clear:
                Debug.Log("gamecear");
                m_audio.Stop();
                Time.timeScale = 0;
                Image im = m_clearImage.GetComponent<Image>();
                Text text = m_clearImage.GetComponentInChildren<Text>();
                if (im.color.a < 1)
                {
                    m_clearImage.SetActive(true);
                    text.text = "Game\nClear";
                    im.color = new Color(im.color.r, im.color.g, im.color.b, alpha);
                    alpha += alphaMaguni;
                }
                else
                {
                    m_status = GameStatus.GameEnd;
                }
                break;
            case GameStatus.GameOver:
                Debug.Log("gameover");
                m_audio.Stop();
                Time.timeScale = 0;
                Image overIm = m_clearImage.GetComponent<Image>();
                Text overText = m_clearImage.GetComponentInChildren<Text>();
                if (overIm.color.a < 1)
                {
                    m_clearImage.SetActive(true);
                    overText.text = "Game\nOver";
                    overIm.color = new Color(overIm.color.r, overIm.color.g, overIm.color.b, alpha);
                    alpha += alphaMaguni;
                }
                else
                {
                    m_status = GameStatus.GameEnd;
                }
                break;
            case GameStatus.GameEnd:
                GeneraterController gc = GameObject.FindGameObjectWithTag("GContr").GetComponent<GeneraterController>();
                gc.StopGenerate();
                Time.timeScale = 1;
                m_status = GameStatus.Debug;
                break;
        }
    }

    
    /// <summary>
    /// アイテムを購入するための関数
    /// </summary>
    /// <param name="cost">消費するコスト</param>
    public void SubtractResourcePoint(int cost)
    {
        if (m_resourcePoint >= cost)
        {
            m_resourcePoint -= cost;
            RefleshPointText();
        }
        else
        {
            Debug.Log("ポイントが足りません");
        }
    }
    /// <summary>
    /// アイテムを返還するための関数
    /// </summary>
    /// <param name="returnCost">返還されるコスト</param>
    public void AddResourcePoint(int returnCost)
    {   
            m_resourcePoint += returnCost;
            RefleshPointText();
    }

    public void RefleshPointText()
    {
        m_resourceText.text = m_resourcePoint.ToString();
    }
    public void DecreceEnemy()
    {
        EnemyNum--;
        m_enemyNumText.text = EnemyNum.ToString();
    }

    public void GetTowerHP()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Tower");
        TowerDeta td = go.GetComponent<TowerDeta>();
        m_towerHPText.text = td.m_towerHP.ToString();
    }

    private void SetEnemyNum()
    {
        GeneraterController gc = GameObject.FindGameObjectWithTag("GContr").GetComponent<GeneraterController>();
        EnemyNum = gc.GetEnemyNum();
    }
}
