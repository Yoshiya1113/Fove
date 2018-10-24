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

    //RaycastHit[] righthits;//右目の当たった座標の格納
    //RaycastHit[] lefthits;//左目の当たった座標の格納
    //Vector3 pos;//

    // Use this for initialization
    void Start () {
        st = DateTime.Now;//実行を始めた時刻
        ts = DateTime.Now;//経過時間代入用の変数
        String now = DateTime.Now.ToString("MMddhhmm");

        //CSVFilePath = @"CSVFile.csv";//csvファイルの名前
        CSVFilePath = now + ".csv";//csvファイルの名前
        streamWriter = new StreamWriter(CSVFilePath);//ファイルの作成
        //csvに書き込むデータの取得
        //csvの一行目に値の名前を書き込む
        streamWriter.Write("StartTime" + "," + "NowTime" + "," + "PathTime"
            + ',' + "hitrX" + ',' + "hitrY" + ',' + "hitrZ"
            + ',' + "hitlX" + ',' + "hitlY" + ',' + "hitlZ");
        streamWriter.WriteLine();//csvに取得したデータを書き込む

    }
	
	// Update is called once per frame
	void Update () {

        /*FoveInterface.EyeRays eyes = FoveInterface.GetEyeRays();//
        RaycastHit hitLeft, hitRight;//hitGaze
        hitRight = new RaycastHit();
        hitLeft = new RaycastHit();*/
        //Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);//左目のレイで最初にあたったものの座標を取る
        //Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);//右目のレイで最初にあたったものの座標を取る
        //righthits = Physics.RaycastAll(eyes.right, 100);//右目のレイにあたったものの全ての座標を取る
        //lefthits = Physics.RaycastAll(eyes.left, 100);//左目のレイにあたったものの全ての座標を取る

        /*switch (FoveInterface.CheckEyesClosed())
        {

            case Fove.EFVR_Eye.Neither:

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);
                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);
                //Physics.Raycast(gaze.ray, out hitGaze, Mathf.Infinity);
                Debug.Log("NotWink");
                Debug.Log("Right:" + eyes.right);
                Debug.Log("Hit_R:" + hitRight.point);
                Debug.Log("Hit_R_3" + eyes.right.GetPoint(3.0f));
                Debug.Log("Hit_R_10" + eyes.right.GetPoint(10.0f));
                Debug.Log("Left:" + eyes.left);
                Debug.Log("Hit_L" + hitLeft.point);
                Debug.Log("Hit_L_3" + eyes.left.GetPoint(3.0f));
                Debug.Log("Hit_L_10" + eyes.left.GetPoint(10.0f));
                break;

            case Fove.EFVR_Eye.Left:

                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);
                //Physics.Raycast(gaze.ray, out hitGaze, Mathf.Infinity);
                Debug.Log("LeftWink");
                Debug.Log("Right:" + eyes.right);
                Debug.Log("Hit_R:" + hitRight.point);
                break;
            case Fove.EFVR_Eye.Right:

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);
                //Physics.Raycast(gaze.ray, out hitGaze, Mathf.Infinity);
                Debug.Log("RightWink");
                Debug.Log("Left:" + eyes.left);
                Debug.Log("Hit_L" + hitLeft.point);
                break;
        }*/



        if (Input.GetKeyDown(KeyCode.Space))
        {
            tf += 1;
        }

        if (tf == 1)//csvへの書き込み
        {
            nt = DateTime.Now;//ここに到達したときの時刻を取得する
            TimeSpan ts = nt - st;//実行からどれくらい経過しているのかを計算
            sst = nt.ToString();//現在時刻を文字列に変換
            snt = nt.ToString();//現在時刻を文字列に変換

            streamWriter.Write(sst + ',' + snt + ',' + ts);//csvに書き込むデータのリスト
            streamWriter.WriteLine();//データの取得
            Debug.Log("書き込み中");
            //Debug.Log(st);//データ確認用
            //Debug.Log(ts);
            //Debug.Log(nt);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            streamWriter.Close();//csvに書き込む
            Debug.Log("書き込み終了");
        }
        /*dt = DateTime.Now;//ここに到達したときの時刻を記録する
        TimeSpan ts = nt - st;//実行からどれくらい経過しているのかを計算
        Debug.Log(st);
        Debug.Log(ts);
        Debug.Log(nt);*/
    }
}
