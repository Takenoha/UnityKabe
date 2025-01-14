using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeforeCountdown : MonoBehaviour
{
    public Text countdownText; // UI Text コンポーネントをアタッチするための変数
    public int countdownTime = 3; // カウントダウン時間（秒）
    public static class GlobalVariables
    {
        public static bool startFlag = False;
    }
    private void Start()
    {
        
    }
    void Update()
    {
        if (startFlag == True)
        {
            StartCoroutine(StartCountdown());
        }
    }
    private IEnumerator StartCountdown()
    {
        // カウントダウンのループ
        while (countdownTime > 0)
        {
            // 現在のカウントを表示
            countdownText.text = countdownTime.ToString();

            // 1秒待つ
            yield return new WaitForSeconds(1f);

            // カウントを減らす
            countdownTime--;
        }

        // カウントダウン終了後の処理
        countdownText.text = "Start!";

        // 1秒待ってからテキストを非表示にする
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        startFlag = True;
    }

    private void StartGame()
    {
        // ゲーム開始時の処理
        Debug.Log("Game Started!");
        // ゲーム開始の処理をここに追加
    }
}
