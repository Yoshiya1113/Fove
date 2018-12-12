using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//CSVファイル出力用
using System;//タイムスタンプ用

public class SpinRoom : MonoBehaviour {

    private int spintim = 0;//状態管理
    private float n = 30.0f;//角速度
    private DateTime spinstart;//開始時刻
    private DateTime nt;//現在時刻
    private DateTime ts;//経過時間
    private float corepathtime;
    private float spinangle = 0.0f;//チェッカールームの回り具合

    private int frameCount;//fps計算用
    private float prevTime;//fps計算用
    private float fpstime;//fps計算用

    public string CSVFilePath;//CSV
    private StreamWriter streamWriter;//CSV

    private Vector3 hmdpos;//HMDの位置
    private Quaternion hmdrot;//HMDの向き

    private Vector3 originhit;//視点

    private int vectionfeel;

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;//fpsの固定

        ts = DateTime.Now;//経過時間代入用の変数
        String now = DateTime.Now.ToString("MMddhhmm");

        CSVFilePath = now + ".csv";//csvファイルの名前
        streamWriter = new StreamWriter(CSVFilePath);//ファイルの作成
        //csvに書き込むデータの取得
        //csvの一行目に値の名前を書き込む
        //CSVに記録する情報
        //現在時刻，現在時刻のミリ秒，経過時間，経過時間のミリ秒，fps，チェッカールームの回転速度，眼球の座標(左)，眼球の座標(右)，視線のベクトル(左)，視線のベクトル(右)，視点の座標，HMDの座標，HMDの向き，ベクション
        streamWriter.Write("StartTime" + "," + "StartMilli" + "," + "PathTime" + "," + "PathMilli"+ ','
            + "FPS" + ','//fps
            + "SpinSpeed" + ','//
            + "EyeLeftX" + ',' + "EyeLeftY" + ',' + "EyeLeftY" + ','//左目眼球の位置
            + "EyeRightX" + ',' + "EyeRightX" + ',' + "EyeRightX" + ','//右目眼球の位置
            + "EyeLeftvecX" + ',' + "EyeLeftvecY" + ',' + "EyeLeftvecY" + ','//左目眼球の位置
            + "EyeRightXvec" + ',' + "EyeRightvecX" + ',' + "EyeRightvecX" + ','//右目眼球の位置
            + "hitX" + ',' + "hitY" + ',' + "hitZ"+ ',' //粗点の座標
            + "HMDposX" + ',' + "HMDposY" + ',' + "HMDposZ" + ','
            + "HMDrotX" + ',' + "HMDrotY" + ',' + "HMDrotZ" + ','
            + "vection"
            );
        streamWriter.WriteLine();//csvに取得したデータを書き込む

        frameCount = 0;//fps計算の準備
        prevTime = 0.0f;//fps計算の準備

        vectionfeel = 0;//ベクションの感知
    }
	
	// Update is called once per frame
	void Update () {
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
        //視点の計算
        RaycastHit hitLeft, hitRight;
        Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);
        Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);
        originhit = hitLeft.point + ((hitRight.point - hitLeft.point) / 2);//視点座標を代入

        //HMD関連のデータの取得
        hmdpos = FoveInterface.GetHMDPosition();//HMDの位置座標
        hmdrot = FoveInterface.GetHMDRotation();//HMDの方向座標
        //Debug.Log(hmdpos.x + "," + hmdpos.y + "," + hmdpos.z + "," + hmdrot.x + "," + hmdrot.y + "," + hmdrot.z + "," + hmdrot.w);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            spintim = 1;
            n = 90.0f;
            spinstart = DateTime.Now;//実行を始めた時刻
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            vectionfeel = 1;
            //n = 90.0f;
        }

        if (spintim == 1)//checkerroomの回転、csvへの書き込み
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

            //CSVに記録する情報
            //現在時刻，現在時刻のミリ秒，経過時間，経過時間のミリ秒，fps，チェッカールームの回転速度，眼球の座標(左)，眼球の座標(右)，視線のベクトル(左)，視線のベクトル(右)，視点の座標，HMDの座標，HMDの向き，ベクション
            streamWriter.Write(nt.ToString() + ',' + nt.Millisecond.ToString() + ',' + ts.ToString() + ',' + nt.Millisecond.ToString() + ','
                + fpstime.ToString() + ','//fps
                + n.ToString() + ','//チェッカールームの回転速度
                + eyes.left.origin.x.ToString() + ',' + eyes.left.origin.y.ToString() + ',' + eyes.left.origin.z.ToString() + ','//左目の座標
                + eyes.right.origin.x.ToString() + ',' + eyes.right.origin.y.ToString() + ',' + eyes.right.origin.z.ToString() + ','//右目の座標
                + FoveInterface.GetLeftEyeVector().x.ToString() + ',' + FoveInterface.GetLeftEyeVector().y.ToString() + ',' + FoveInterface.GetLeftEyeVector().z.ToString() + ','//左目ベクトル
                + FoveInterface.GetRightEyeVector().x.ToString() + ',' + FoveInterface.GetRightEyeVector().y.ToString() + ',' + FoveInterface.GetRightEyeVector().z.ToString() + ','//右目ベクトル
                + originhit.x.ToString() + ',' + originhit.y.ToString() + ',' + originhit.z.ToString() + ',' //視点の座標
                + hmdpos.x.ToString() + ',' + hmdpos.y.ToString() + ',' + hmdpos.z.ToString() + ','//HMDの座標
                + hmdrot.x.ToString() + ',' + hmdrot.y.ToString() + ',' + hmdrot.z.ToString() + ',' + hmdrot.w.ToString() + ','//HMDの向き
                + vectionfeel.ToString());//ベクション
            //csvに書き込むデータのリスト
            streamWriter.WriteLine();//改行
            Debug.Log("書き込み中");

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
            streamWriter.Close();//csvに書き込む
            //Debug.Log("書き込み終了");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            n = 180.0f;
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
