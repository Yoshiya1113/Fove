using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunneringMouseControler : MonoBehaviour {

    public Material plane;//マテリアルの変数
    public Vector2 pos;//位置の変数
    private int tf = 0;//spaceコントロール用
    private float tsize = 3.3F;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var mousemove = Input.mousePosition;//マウスの座標の取得
        pos = mousemove / new Vector2(Screen.width, Screen.height);
        Debug.Log("Mousemove" + mousemove);
        //Debug.Log("FirstPos" + pos);
        Debug.Log("FirstPos(" + pos.x + "," + pos.y + ")");
        //マウスの座標をシェーダーに代入するために値を調整
        //1～0で表現するためにスクリーンの大きさで割る
        pos -= new Vector2(0.5f, 0.5f);//中心座標のずれを修正

        plane.SetFloat("_UX", pos.x);//マウスのx座標をシェーダーのx座標に代入
        plane.SetFloat("_VY", pos.y);//マウスのy座標をシェーダーのx座標に代入
        plane.SetFloat("_Size", tsize);//トンネリングの円のサイズを設定

        //Debug.Log("Pos" + pos);
        Debug.Log("Pos(" + pos.x + "," + pos.y + ")");


        if (Input.GetKeyDown(KeyCode.B))//Bを押すとトンネリングのサイズが大きくなる
        {
            tf += 1;
            tsize -= 0.1F;
        }

        if (tf == 20)//一定回数以上大きくするともとにもどる
        {
            tf = 0;
            tsize = 3.3F;
        }
        //Debug.Log("tf&tsize(" + tf + "," + tsize + ")");
    }
}
