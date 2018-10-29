using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//タイムスタンプ用

public class SpinRoom : MonoBehaviour {

    private int spintim = 0;//状態管理
    private DateTime spinstart;//開始時刻
    private DateTime nt;//現在時刻
    private DateTime ts;//経過時間
    private float corepathtime;
    private float spinangle;
    
    //private string sst;//開始時刻(string型)
    //private string snt;//現在時刻(string型)
                       // Use this for initialization
    void Start () {
        spinstart = DateTime.Now;//実行を始めた時刻
        ts = DateTime.Now;//経過時間代入用の変数
        String now = DateTime.Now.ToString("MMddhhmm");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spintim += 1;
        }

        if (spintim == 1)//csvへの書き込み
        {
            nt = DateTime.Now;//ここに到達したときの時刻を取得する
            TimeSpan ts = nt - spinstart;//実行からどれくらい経過しているのかを計算
            //corepathtime = ts.ToString("FFFFFFF");
            //transform.rotation = (0, 0, ts*90);
            //spinangle = ts * 90;
            //transform.localRotation = Quaternion.Euler(0.0f, ts * 90f, 0.0f);

            //sst = nt.ToString();//現在時刻を文字列に変換
            //snt = nt.ToString();//現在時刻を文字列に変換


            //Debug.Log(st);//データ確認用
            //Debug.Log(ts);
            //Debug.Log(nt);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            
            //Debug.Log("書き込み終了");
        }
    }
}
