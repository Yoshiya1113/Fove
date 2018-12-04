using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//タイムスタンプ用

public class SpinRoom : MonoBehaviour {

    private int spintim = 0;//状態管理
    private float n = 30.0f;//角速度
    private DateTime spinstart;//開始時刻
    private DateTime nt;//現在時刻
    private DateTime ts;//経過時間
    private float corepathtime;
    private float spinangle = 0.0f;//チェッカールームの回り具合
    
    //private string sst;//開始時刻(string型)
    //private string snt;//現在時刻(string型)
                       // Use this for initialization
    void Start () {    
        ts = DateTime.Now;//経過時間代入用の変数
        String now = DateTime.Now.ToString("MMddhhmm");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spintim = 1;
            spinstart = DateTime.Now;//実行を始めた時刻
        }

        if (spintim == 1)//csvへの書き込み
        {
            nt = DateTime.Now;//ここに到達したときの時刻を取得する
            TimeSpan ts = nt - spinstart;//実行からどれくらい経過しているのかを計算

            //経過時刻と角速度に合わせてチェッカールームを傾ける
            //フレーム当たりの角速度を
            spinangle = ts.Milliseconds * n / 1000.0f;
            spinangle += ts.Seconds * n;
            spinangle += ts.Minutes * n;
            //spinangle += 0.9f;//Unityは60fpsというのを前提
            transform.localRotation = Quaternion.Euler(0.0f, spinangle, 0.0f);

            //spinstart = nt;

            //データ確認用
            //Debug.Log(spinstart);//回転開始時
            //Debug.Log(nt);//現在時刻
            //Debug.Log(ts);//経過時間
            //Debug.Log(ts.Milliseconds);//経過時間(ミリ秒)
            //Debug.Log(spinangle);//移動角
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            spintim = 0;
            //Debug.Log("書き込み終了");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            n = 30.0f;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            n = 60.0f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            n = 90.0f;
        }
    }
}
