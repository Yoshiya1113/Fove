using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//√の計算用

public class Fove3DCursorVector : MonoBehaviour {
    private Vector3 origineye;
    private Vector3 originvector;
    private Vector3 tunneringpos;
    //private float root10 = 3.162f;//計算用の√10の値
    private float root10 = 10.0f;//計算用の√10の値
    //private float kyori;

    //else用不要な計算？
    private Vector3 originhit;//視点
    private float origindirection;//視点までの距離の2乗
    private float origindirection2;//視点までの距離の
    private float origindirection3;//視点までの距離の√
    private Vector3 newtunnering;//トンネリング座標
    private float kyori;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FoveInterface.EyeRays eyes = FoveInterface.GetEyeRays();
        RaycastHit hitLeft, hitRight;

        switch (FoveInterface.CheckEyesClosed())
        {
            case Fove.EFVR_Eye.Neither:

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);
                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);
                if (hitLeft.point != Vector3.zero && hitRight.point != Vector3.zero)
                {
                    origineye = (eyes.left.origin + eyes.right.origin) / 2;//眼球の中間の座標を求める
                    originvector = (FoveInterface.GetLeftEyeVector() + FoveInterface.GetRightEyeVector()) / 2;//両目のベクトルの平均を求める
                    tunneringpos = root10 * originvector + origineye;//眼球の座標と大きさ10のベクトルでトンネリングの座標を求める
                    transform.position =　tunneringpos;//トンネリングを移動

                    kyori = tunneringpos.x * tunneringpos.x + tunneringpos.y * tunneringpos.y + tunneringpos.z * tunneringpos.z;
                    Debug.Log(origineye.ToString());
                    Debug.Log(originvector.ToString());
                    Debug.Log(tunneringpos.ToString());
                    //Debug.Log(tunneringpos.x.ToString() + ":" + tunneringpos.y.ToString() + ":" + tunneringpos.z.ToString() + ":" + kyori.ToString());

                }
                else
                {
                    originhit = eyes.left.GetPoint(3.0f) + ((eyes.right.GetPoint(3.0f) - eyes.left.GetPoint(3.0f)) / 2);//視点を代入
                    origindirection = originhit.x * originhit.x + originhit.y * originhit.y + originhit.z * originhit.z;//距離の2乗を計算
                    origindirection2 = Mathf.Sqrt(origindirection);
                    origindirection3 = Mathf.Sqrt(origindirection2);
                    newtunnering = new Vector3(originhit.x * root10 / origindirection3, originhit.y * root10 / origindirection3, originhit.z * root10 / origindirection3);//トンネリングの座標を計算して代入
                    transform.position = newtunnering;//トンネリングを移動
                }
                break;
            case Fove.EFVR_Eye.Left:

                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);
                if (hitRight.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
                {
                    origineye = eyes.right.origin;//右目の座標を求める
                    originvector = FoveInterface.GetRightEyeVector();//右目のベクトルの平均を求める
                    tunneringpos = root10 * originvector + origineye;//眼球の座標と大きさ10のベクトルでトンネリングの座標を求める
                    transform.position = tunneringpos;//トンネリングを移動

                    kyori = tunneringpos.x * tunneringpos.x + tunneringpos.y * tunneringpos.y + tunneringpos.z * tunneringpos.z;
                    Debug.Log(tunneringpos.x.ToString() + ":" + tunneringpos.y.ToString() + ":" + tunneringpos.z.ToString() + ":" + kyori.ToString());


                }
                else
                {
                    originhit = eyes.right.GetPoint(3.0f);//視点を代入
                    origindirection = originhit.x * originhit.x + originhit.y * originhit.y + originhit.z * originhit.z;//距離の2乗を計算
                    origindirection2 = Mathf.Sqrt(origindirection);
                    origindirection3 = Mathf.Sqrt(origindirection2);
                    newtunnering = new Vector3(originhit.x * root10 / origindirection3, originhit.y * root10 / origindirection3, originhit.z * root10 / origindirection3);//トンネリングの座標を計算して代入
                    transform.position = newtunnering;//トンネリングを移動
                }
                break;
            case Fove.EFVR_Eye.Right:

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);
                if (hitLeft.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
                {
                    origineye = eyes.left.origin;//左目の座標を求める
                    originvector = FoveInterface.GetLeftEyeVector();//左目のベクトルの平均を求める
                    tunneringpos = root10 * originvector + origineye;//眼球の座標と大きさ10のベクトルでトンネリングの座標を求める
                    transform.position = tunneringpos;//トンネリングを移動

                    kyori = tunneringpos.x * tunneringpos.x + tunneringpos.y * tunneringpos.y + tunneringpos.z * tunneringpos.z;
                    Debug.Log(tunneringpos.x.ToString() + ":" + tunneringpos.y.ToString() + ":" + tunneringpos.z.ToString() + ":" + kyori.ToString());


                }
                else
                {
                    originhit = eyes.left.GetPoint(3.0f);//視点を代入
                    origindirection = originhit.x * originhit.x + originhit.y * originhit.y + originhit.z * originhit.z;//距離の2乗を計算
                    origindirection2 = Mathf.Sqrt(origindirection);
                    origindirection3 = Mathf.Sqrt(origindirection2);
                    newtunnering = new Vector3(originhit.x * root10 / origindirection3, originhit.y * root10 / origindirection3, originhit.z * root10 / origindirection3);//トンネリングの座標を計算して代入
                    transform.position = newtunnering;//トンネリングを移動
                }
                break;
        }

    }
}
