using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // スコア表示用のText
    private int score;     // スコアの変数

    void Start()
    {
        score = 0; // スコアを初期化
        UpdateScoreText(); // スコア表示を更新
    }

    void Update()
    {
        // スペースキーが押されたときにスコアを加算
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddScore();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            score = 0;
            UpdateScoreText(); // スコア表示を更新
        }
    }

    // スコアを加算するメソッド
    public void AddScore()
    {
        score += 100; // スコアを100加算
        UpdateScoreText(); // スコア表示を更新
    }

    // スコア表示を更新するメソッド
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // テキストを更新
    }
}
