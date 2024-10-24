
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Prefabs; // 生成するプレファブの配列
    private float time; // タイマー用の変数
    private int number; // ランダムに選ばれたプレファブのインデックス
    //スタートと終わりの目印
    public Transform startMarker;
    public Transform endMarker;

    // スピード
    public float speed = 1.0F;

    //二点間の距離を入れる
    private float distance_two;
    void Start()
    {
         //二点間の距離を代入(スピード調整に使う)
        distance_two = Vector3.Distance(startMarker.position, endMarker.position);
        time = 1.0f; // Startが呼ばれた時、タイマーを1秒に設定
        Vector3 start = transform.position;
        Vector3 target = new Vector3(10, -5, 0);
    }

    void Update()
    {
        time -= Time.deltaTime; // タイマーを減少させる
        if (time <= 0.0f) // タイマーが0以下になったら
        {
            time = 1.0f; // タイマーをリセット
            number = Random.Range(0, Prefabs.Length); // プレファブ配列からランダムにインデックスを選ぶ
            Instantiate(Prefabs[number], new Vector3(0, 0, 0), Quaternion.identity); // 選ばれたプレファブを生成
        }

            float present_Location = (Time.time * speed) / distance_two;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, present_Location); // timerの変化分だけ移動させる（1秒で目的地）
    }
}