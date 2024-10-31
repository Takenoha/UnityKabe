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

    // 二点間の距離を入れる
    private float distance_two;

    // プレファブが移動するための変数
    public GameObject spawnedPrefab; // 生成されたプレファブの参照
    private float journeyLength;
    private float startTime;

    // プレファブの大きさを指定するためのパラメータ
    public Vector3 prefabScale = new Vector3(1, 1, 1); // デフォルトサイズ (1, 1, 1)

    // 各キーに対応する音声クリップ
    public AudioClip TrueSound;
    public AudioClip FalseSound;

    private AudioSource audioSource;

    // プレハブの生成を管理するフラグ
    private bool isSpawningEnabled = false;

    // マテリアルをクラスのフィールドとして定義
    private Material redmat;
    private Material greenmat;

    void Start()
    {
        // 二点間の距離を代入(スピード調整に使う)
        distance_two = Vector3.Distance(startMarker.position, endMarker.position);
        time = 1.0f; // Startが呼ばれた時、タイマーを1秒に設定

        // AudioSourceコンポーネントの取得
        audioSource = gameObject.AddComponent<AudioSource>();

        // マテリアルを読み込み
        redmat = (Material)Resources.Load("Red");
        greenmat = (Material)Resources.Load("Green");
    }

    void Update()
    {
        // 任意のキー入力で生成のオン/オフを切り替える
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSpawningEnabled = !isSpawningEnabled;
            Destroy(spawnedPrefab);
            time = 1.0F;
        }

        if (isSpawningEnabled)
        {
            time -= Time.deltaTime; // タイマーを減少させる
            if (time <= 0.0f) // タイマーが0以下になったら
            {
                time = 4.0f; // タイマーをリセット

                // 前のプレファブが存在するなら削除
                if (spawnedPrefab != null)
                {
                    Destroy(spawnedPrefab);
                }

                number = Random.Range(0, Prefabs.Length); // プレファブ配列からランダムにインデックスを選ぶ

                // 新しいプレファブをstartMarkerの位置に生成
                spawnedPrefab = Instantiate(Prefabs[number], startMarker.position, Quaternion.identity);

                // 生成されたプレファブの大きさを指定
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

                // キー入力で色を変更し、対応する音を再生
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //ChangeMaterial(redmat);
                    PlaySound(FalseSound);
                }
                else if (Input.GetKeyDown(KeyCode.T))
                {
                    //ChangeMaterial(greenmat);
                    PlaySound(TrueSound);
                }
            }
        }
    }

    // プレファブのマテリアルを変更するメソッド
    void ChangeMaterial(Material newMaterial)
    {
        if (spawnedPrefab != null)
        {
            Renderer renderer = spawnedPrefab.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = newMaterial;
            }
            else
            {
                Debug.LogWarning("Renderer component is missing on " + spawnedPrefab.name);
            }
        }
    }

    // プレファブの色を変更するメソッド（現在は使用されていません）
    void ChangeColor(Color color)
    {
        if (spawnedPrefab != null)
        {
            Renderer renderer = spawnedPrefab.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }

    // 指定された音声クリップを再生するメソッド
    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
