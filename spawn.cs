using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] Prefabs; // 生成するプレファブの配列
    private float time; // タイマー用の変数
    private int number; // ランダムに選ばれたプレファブのインデックス
    // スタートと終わりの目印
    public Transform startMarker;
    public Transform endMarker;

    // スピード
    public float speed = 1.0F;

    public float otime = 1.0f;
    // 二点間の距離を入れる
    private float distance_two;
    
    // プレファブが移動するための変数
    private GameObject spawnedPrefab; // 生成されたプレファブの参照
    private float journeyLength;
    private float startTime;
    
    //おおきさ　
    public Vector3 prefabScale = new Vector3(1, 1, 1); // デフォルトサイズ (1, 1, 1)

    void Start()
    {
        // 二点間の距離を代入(スピード調整に使う)
        distance_two = Vector3.Distance(startMarker.position, endMarker.position);
        time = 1.0F; // Startが呼ばれた時、タイマーを1秒に設定
    }

    void Update()
    {
        time -= Time.deltaTime; // タイマーを減少させる
        if (time <= 0.0f) // タイマーが0以下になったら
        {
            time = otime; // タイマーをリセット

            // 前のプレファブが存在するなら削除
            if (spawnedPrefab != null)
            {
                Destroy(spawnedPrefab);
            }

            number = Random.Range(0, Prefabs.Length); // プレファブ配列からランダムにインデックスを選ぶ

            // 新しいプレファブをstartMarkerの位置に生成
            spawnedPrefab = Instantiate(Prefabs[number], startMarker.position, Quaternion.identity);
            spawnedPrefab.transform.localScale = prefabScale;
            // プレファブの移動に必要な初期化
            startTime = Time.time;
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        }

        // プレファブが生成された場合のみ移動
        if (spawnedPrefab != null)
        {
            // 生成されてからの経過時間に基づいて移動
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;

            // プレファブを移動させる
            spawnedPrefab.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        }
    }
}