using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameStatus m_status;
    [SerializeField] int m_rimit = 3;
    int m_score = 0;
    //アイテムを購入するのに必要なポイント
    [SerializeField] int m_resourcePoint = 1200;
    //リソースポイントを表示するテキスト
    [SerializeField] Text m_resourceText;
    // Start is called before the first frame update
    void Start()
    {
        m_status = GameStatus.initiate;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_status == GameStatus.Debug) return;
        
        switch (m_status)
        {
            case GameStatus.initiate:
                Debug.Log("initiate");
                m_resourceText.text = m_resourcePoint.ToString();
                m_status = GameStatus.gameStart;
                break;
            case GameStatus.gameStart:
                Debug.Log("gamestart");
                m_status = GameStatus.nowGame;
                break;
            case GameStatus.nowGame:
                if (m_score >= m_rimit)
                {
                    m_status = GameStatus.Clear;
                }
                break;
            case GameStatus.Clear:
                Debug.Log("gamecear");
                m_status = GameStatus.GameOver;
                break;
            case GameStatus.GameOver:
                Debug.Log("gameover");
                m_status = GameStatus.Debug;
                break;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            AddScore();
        }
    }

    /// <summary>
    /// ゲームの進行状態を表すenum
    /// </summary>
    public  enum  GameStatus
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
        /// <summary>デバッグ用</summary>
        Debug
    }

    /// <summary>
    /// シーンを移行するための関数
    /// </summary>
    /// <param name="sceneName">移行先のシーン名</param>
    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// スコアを加算する関数
    /// </summary>
    public void AddScore()
    {
        m_score++;
    }

    /// <summary>
    /// アイテムを購入するための関数
    /// </summary>
    /// <param name="cost">消費するコスト</param>
    public void SubtractResourcePoint(int cost)
    {
        m_resourcePoint -= cost;
    }
    /// <summary>
    /// アイテムを返還するための関数
    /// </summary>
    /// <param name="returnCost">返還されるコスト</param>
    public void AddResourcePoint(int returnCost)
    {
        m_resourcePoint += returnCost;
    }

    public void RefleshPointText()
    {
        m_resourceText.text = m_resourcePoint.ToString();
    }
}
