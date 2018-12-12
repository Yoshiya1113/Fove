using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//√の計算用

public class Fove3DCursor : MonoBehaviour {

    private Vector3 eyerighttunnering;//右目トンネリングの座標
    private Vector3 eyerightdistance;//右目から見たトンネリングの座標
    private Vector3 eyelefttunnering;//左目トンネリングの座標
    private Vector3 eyeleftdistance;//左目から見たトンネリングの座標
    private Vector3 eyetunnering;//トンネリングの座標
    private Vector3 eyedistance;//両目から見たトンネリングの座標
    private float kyori;

    private float root10 = 3.162f;

    // Use this for initialization
    void Start () {
        //root10 = Math.Sqrt(10);
	}
	
	// Update is called once per frame
	void Update () {
        FoveInterface.EyeRays eyes = FoveInterface.GetEyeRays();
        RaycastHit hitLeft, hitRight;

        switch (FoveInterface.CheckEyesClosed())//瞬き検知
        {
            case Fove.EFVR_Eye.Neither://両目が開いているとき

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);//左目のraycastの取得
                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);//右目のraycastの取得
                if (hitLeft.point != Vector3.zero && hitRight.point != Vector3.zero)
                {
                    eyerightdistance = eyes.right.direction * root10;//右目から見たトンネリングの座標を計算
                    eyerighttunnering = eyes.right.origin + eyerightdistance;//右目の座標と合わせることでトンネリングの正しい位置を出す
                    eyeleftdistance = eyes.left.direction * root10;//右目から見たトンネリングの座標を計算
                    eyelefttunnering = eyes.left.origin + eyeleftdistance;//右目の座標と合わせることでトンネリングの正しい位置を出す
                    eyetunnering = (eyerighttunnering + eyelefttunnering) / 2;//両目のトンネリング座標から、間の座標を出す
                    transform.position = eyetunnering;//座標に移動
                    //Debug.Log(eyetunnering.x.ToString() + '+' + eyetunnering.y.ToString() + '+' + eyetunnering.z.ToString());
                    //kyori = eyetunnering.x * eyetunnering.x + eyetunnering.y * eyetunnering.y + eyetunnering.z * eyetunnering.z;
                    //Debug.Log(kyori.ToString());

                }
                else
                {
                    transform.position = eyes.left.GetPoint(3.0f) + ((eyes.right.GetPoint(3.0f) - eyes.left.GetPoint(3.0f)) / 2);
                }
                break;
            case Fove.EFVR_Eye.Left://左目が閉じているとき

                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);//右目のraycastの取得
                if (hitRight.point != Vector3.zero)
                {
                    eyerightdistance = eyes.right.direction * root10;//右目から見たトンネリングの座標を計算
                    eyerighttunnering = eyes.right.origin + eyerightdistance;//右目の座標と合わせることでトンネリングの正しい位置を出す
                    transform.position = eyerighttunnering;//指定座標に移動
                    //Debug.Log(eyerighttunnering.x.ToString() + '+' + eyerighttunnering.y.ToString() + '+' + eyerighttunnering.z.ToString());
                }
                else
                {
                    transform.position = eyes.right.GetPoint(3.0f);
                }
                break;
            case Fove.EFVR_Eye.Right://右目閉じているとき

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);//左目のraycastの取得
                if (hitLeft.point != Vector3.zero)
                {
                    eyeleftdistance = eyes.left.direction * root10;//右目から見たトンネリングの座標を計算
                    eyelefttunnering = eyes.left.origin + eyeleftdistance;//右目の座標と合わせることでトンネリングの正しい位置を出す
                    transform.position = eyelefttunnering;//指定座標に移動
                    //Debug.Log(eyelefttunnering.x.ToString() + '+' + eyelefttunnering.y.ToString() + '+' + eyelefttunnering.z.ToString());
                }
                else
                {
                    transform.position = eyes.left.GetPoint(3.0f);
                }
                break;
        }
    }
}
