using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//CSVファイル出力用
using System;//タイムスタンプ用

public class CSVRecorder1 : MonoBehaviour {
    public string CSVFilePath;//CSV
    private StreamWriter streamWriter;//CSV
    private DateTime st;//開始時刻
    private DateTime nt;//現在時刻
    private DateTime ts;//経過時間
    private string sst;//開始時刻(string型)
    private string snt;//現在時刻(string型)
    private int tf = 0;//
    private Vector3 hmdpos;
    private Quaternion hmdrot;
    private int frameCount;//fps計算用
    private float prevTime;//fps計算用
    private float fpstime;//fps計算用

    //RaycastHit[] righthits;//右目の当たった座標の格納
    //RaycastHit[] lefthits;//左目の当たった座標の格納
    //Vector3 pos;//

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;

        st = DateTime.Now;//実行を始めた時刻
        ts = DateTime.Now;//経過時間代入用の変数
        String now = DateTime.Now.ToString("MMddhhmm");

        //CSVFilePath = @"CSVFile.csv";//csvファイルの名前
        CSVFilePath = now + ".csv";//csvファイルの名前
        streamWriter = new StreamWriter(CSVFilePath);//ファイルの作成

        //csvに書き込むデータの取得
        //csvの一行目に値の名前を書き込む
        //CSVに記録する情報
        //現在時刻，ミリ秒，経過時間，チェッカールームの回転速度，眼球の座標(左)，眼球の座標(右)，視線のベクトル(左)，視線のベクトル(右)，HMDの座標，HMDの向き
        streamWriter.Write("StartTime" + "," + "NowTime" + "," + "PathTime"
            + ',' + "hitrX" + ',' + "hitrY" + ',' + "hitrZ"
            + ',' + "hitlX" + ',' + "hitlY" + ',' + "hitlZ");
        streamWriter.WriteLine();//csvに取得したデータを書き込む

        frameCount = 0;
        prevTime = 0.0f;

    }
	
	// Update is called once per frame
	void Update () {
        //時間関連データの取得
        nt = DateTime.Now;//ここに到達したときの時刻を取得する
        TimeSpan ts = nt - st;//実行からどれくらい経過しているのかを計算

        //fpsの計算
        ++frameCount;
        float time = Time.realtimeSinceStartup - prevTime;
        if (time >= 0.5f)
        {
            fpstime = frameCount / time;
            //Debug.LogFormat("{0}fps", fpstime);
            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }

        //目のデータの取得
        FoveInterface.EyeRays eyes = FoveInterface.GetEyeRays();

        //HMD関連のデータの取得
        hmdpos = FoveInterface.GetHMDPosition();//HMDの位置座標
        hmdrot = FoveInterface.GetHMDRotation();//HMDの方向座標
        //Debug.Log(hmdpos.x + "," + hmdpos.y + "," + hmdpos.z + "," + hmdrot.x + "," + hmdrot.y + "," + hmdrot.z + "," + hmdrot.w);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            tf = 1;
        }

        if (tf == 1)//csvへの書き込み
        {
            //CSVに記録する情報
            //現在時刻，現在時刻のミリ秒，経過時間，経過時間のミリ秒，fps，チェッカールームの回転速度，眼球の座標(左)，眼球の座標(右)，視線のベクトル(左)，視線のベクトル(右)，HMDの座標，HMDの向き
            streamWriter.Write(nt.ToString() + ',' + nt.Millisecond.ToString() + ',' + ts.ToString() + ',' + nt.Millisecond.ToString() + ','
                + eyes.right.origin.x.ToString() + ',' + eyes.right.origin.y.ToString() + ',' + eyes.right.origin.z.ToString() + ','
                + eyes.left.origin.x.ToString() + ',' + eyes.left.origin.y.ToString() + ',' + eyes.left.origin.z.ToString() + ','
                + hmdpos.x.ToString() + ',' + hmdpos.y.ToString() + ',' + hmdpos.z.ToString() + ','
                + hmdrot.x.ToString() + ',' + hmdrot.y.ToString() + ',' + hmdrot.z.ToString() + ',' + hmdrot.w.ToString());//csvに書き込むデータのリスト
            streamWriter.WriteLine();//データの取得
            Debug.Log("書き込み中");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            streamWriter.Close();//csvに書き込む
            Debug.Log("書き込み終了");
        }
    }
}
