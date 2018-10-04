using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunneringEyeControler : MonoBehaviour {

    public Material plane;//マテリアルの変数
    public Vector2 eyepos;//視線の位置の変数
    public Vector2 tunpos;//トンネリングの位置の変数

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        FoveInterface.EyeRays eyes = FoveInterface.GetEyeRays();
        RaycastHit hitLeft, hitRight;

        switch (FoveInterface.CheckEyesClosed())
        {
            case Fove.EFVR_Eye.Neither:

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);
                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);
                if (hitLeft.point != Vector3.zero && hitRight.point != Vector3.zero)
                {
                    //transform.position = hitLeft.point + ((hitRight.point - hitLeft.point) / 2);

                    eyepos = hitLeft.point + ((hitRight.point - hitLeft.point) / 2);
                    tunpos = eyepos / new Vector2(Screen.width, Screen.height);
                    //マウスの座標をシェーダーに代入するために値を調整
                    //1～0で表現するためにスクリーンの大きさで割る
                    tunpos -= new Vector2(0.5f, 0.5f);//中心座標のずれを修正
                    plane.SetFloat("_UX", tunpos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", tunpos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                else
                {
                    //transform.position = eyes.left.GetPoint(3.0f) + ((eyes.right.GetPoint(3.0f) - eyes.left.GetPoint(3.0f)) / 2);

                    eyepos = eyes.left.GetPoint(3.0f) + ((eyes.right.GetPoint(3.0f) - eyes.left.GetPoint(3.0f)) / 2); ;
                    tunpos = eyepos / new Vector2(Screen.width, Screen.height);
                    //マウスの座標をシェーダーに代入するために値を調整
                    //1～0で表現するためにスクリーンの大きさで割る
                    tunpos -= new Vector2(0.5f, 0.5f);//中心座標のずれを修正
                    plane.SetFloat("_UX", tunpos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", tunpos.y);//マウスのy座標をシェーダーのx座標に代入
                }

                break;
            case Fove.EFVR_Eye.Left:

                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);
                if (hitRight.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
                {
                    //transform.position = hitRight.point;

                    eyepos = hitRight.point;
                    tunpos = eyepos / new Vector2(Screen.width, Screen.height);
                    //マウスの座標をシェーダーに代入するために値を調整
                    //1～0で表現するためにスクリーンの大きさで割る
                    tunpos -= new Vector2(0.5f, 0.5f);//中心座標のずれを修正
                    plane.SetFloat("_UX", tunpos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", tunpos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                else
                {
                    //transform.position = eyes.right.GetPoint(3.0f);

                    eyepos = eyes.right.GetPoint(3.0f);
                    tunpos = eyepos / new Vector2(Screen.width, Screen.height);
                    //マウスの座標をシェーダーに代入するために値を調整
                    //1～0で表現するためにスクリーンの大きさで割る
                    tunpos -= new Vector2(0.5f, 0.5f);//中心座標のずれを修正
                    plane.SetFloat("_UX", tunpos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", tunpos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                break;
            case Fove.EFVR_Eye.Right:

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);
                if (hitLeft.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
                {
                    //transform.position = hitLeft.point;

                    eyepos = hitLeft.point;
                    tunpos = eyepos / new Vector2(Screen.width, Screen.height);
                    //マウスの座標をシェーダーに代入するために値を調整
                    //1～0で表現するためにスクリーンの大きさで割る
                    tunpos -= new Vector2(0.5f, 0.5f);//中心座標のずれを修正
                    plane.SetFloat("_UX", tunpos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", tunpos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                else
                {
                    //transform.position = eyes.left.GetPoint(3.0f);

                    eyepos = eyes.left.GetPoint(3.0f);
                    tunpos = eyepos / new Vector2(Screen.width, Screen.height);
                    //マウスの座標をシェーダーに代入するために値を調整
                    //1～0で表現するためにスクリーンの大きさで割る
                    tunpos -= new Vector2(0.5f, 0.5f);//中心座標のずれを修正
                    plane.SetFloat("_UX", tunpos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", tunpos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                break;
        }

        //Debug.Log("Eyepos" + eyepos);
        //Debug.Log("Tunpos" + tunpos);
        //Debug.Log("Pos(" + pos.x + "," + pos.y + ")");
    }
}
