using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameStatus m_status;
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
                m_status = GameStatus.gameStart;
                break;
            case GameStatus.gameStart:
                Debug.Log("gamestart");
                m_status = GameStatus.nowGame;
                break;
            case GameStatus.nowGame:
                Debug.Log("nowgame");
                m_status = GameStatus.Clear;
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
}
