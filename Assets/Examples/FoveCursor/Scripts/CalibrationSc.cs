using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//CSVファイル出力用

public class CalibrationSc : MonoBehaviour
{
    public Material[] _material;
    public GameObject[] _object;
    private int k = 0;
    Vector3 eyevector;//計測したベクトル
    Vector3 truthvector;//理想のベクトル
    private float theta;

    public string CSVFilePath;//CSV
    private StreamWriter streamWriter;//CSV

    // Use this for initialization
    void Start()
    {
        CSVFilePath = "Calibration.csv";//csvファイルの名前
        streamWriter = new StreamWriter(CSVFilePath);//ファイルの作成
        streamWriter.Write("k" + ","　+ "theta" + ","
            + "eyevectorX" + "," + "eyevectorY" + "," + "eyevectorZ" + ','
            + "truthvectorX" + ',' + "truthvectorY" + ',' + "truthvectorZ");
        streamWriter.WriteLine();//改行

    }

    // Update is called once per frame
    void Update()
    {
        FoveInterface.EyeRays eyes = FoveInterface.GetEyeRays();

        for (int i = 0; i < 9; i++)
        {
            _object[i].GetComponent<Renderer>().material = _material[0];
        }
        k = 0;
        eyevector.x = eyevector.y = eyevector.z = 0;
        truthvector.x = truthvector.y = truthvector.z = 0;

        //デフォルトの状態のとき
        if (k == 0)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                k = 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                k = 2;
            }
            if (Input.GetKey(KeyCode.E))
            {
                k = 3;
            }
            if (Input.GetKey(KeyCode.A))
            {
                k = 4;
            }
            if (Input.GetKey(KeyCode.S))
            {
                k = 5;
            }
            if (Input.GetKey(KeyCode.D))
            {
                k = 6;
            }
            if (Input.GetKey(KeyCode.Z))
            {
                k = 7;
            }
            if (Input.GetKey(KeyCode.X))
            {
                k = 8;
            }
            if (Input.GetKey(KeyCode.C))
            {
                k = 9;
            }
        }

        if (k > 0)
        {
            _object[k-1].GetComponent<Renderer>().material = _material[1];
            //ベクトルの取得
            switch (FoveInterface.CheckEyesClosed())
            {
                case Fove.EFVR_Eye.Neither:
                    eyevector = (FoveInterface.GetLeftEyeVector() + FoveInterface.GetLeftEyeVector()) / 2;
                    truthvector = _object[k - 1].GetComponent<Transform>().position - ((eyes.left.origin + eyes.right.origin) / 2);

                    break;
                case Fove.EFVR_Eye.Left:
                    eyevector.x = eyevector.y = eyevector.z = 0;
                    truthvector.x = truthvector.y = truthvector.z = 0;
                    //eyevector = FoveInterface.GetRightEyeVector();
                    //truthvector = _object[k - 1].GetComponent<Transform>().position - eyes.right.origin;


                    break;
                case Fove.EFVR_Eye.Right:
                    eyevector.x = eyevector.y = eyevector.z = 0;
                    truthvector.x = truthvector.y = truthvector.z = 0;
                    //eyevector = FoveInterface.GetLeftEyeVector();
                    //truthvector = _object[k - 1].GetComponent<Transform>().position - eyes.left.origin;

                    break;
            }
            //なす角の計算
            theta = Mathf.Acos(Vector3.Dot(eyevector, truthvector) / (eyevector.magnitude * truthvector.magnitude)) * Mathf.Rad2Deg;
            Debug.Log(theta);//誤差の表示
            streamWriter.Write(k.ToString() + ',' + theta.ToString() + ','
                + eyevector.x.ToString() + ',' + eyevector.y.ToString() + ',' + eyevector.z.ToString() + ','
                + truthvector.x.ToString() + ',' + truthvector.y.ToString() + ',' + truthvector.z.ToString());
            //csvに書き込むデータのリスト
            streamWriter.WriteLine();//改行


        }
        Debug.Log(k);

    }
}
