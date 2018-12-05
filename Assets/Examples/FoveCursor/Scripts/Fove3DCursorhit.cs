using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fove3DCursorhit : MonoBehaviour {
    private Vector3 originhit;//視点
    private float origindirection;//視点までの距離の2乗
    private float origindirection2;//視点までの距離の
    private float origindirection3;//視点までの距離の√
    private Vector3 newtunnering;//トンネリング座標
    private float root10 = 3.162f;//計算用の√10の値
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
                    originhit = hitLeft.point + ((hitRight.point - hitLeft.point) / 2);//視点座標を代入
                    //眼球の中心座標を求める
                    //視点の座標から眼球の中心座標を引いて、眼球から視点までのベクトルを求める
                    origindirection = originhit.x * originhit.x + originhit.y * originhit.y + originhit.z * originhit.z;//距離の2乗を計算
                    origindirection2 = Mathf.Sqrt(origindirection);//√計算
                    origindirection3 = Mathf.Sqrt(origindirection2);//√計算
                    newtunnering = new Vector3(originhit.x * root10 / origindirection3, originhit.y * root10 / origindirection3, originhit.z * root10 / origindirection3);//長さ10のベクトルに変換
                    //眼球の座標にトンネリングまでの座標を足す
                    transform.position = newtunnering;//トンネリングを移動
                    kyori = newtunnering.x * newtunnering.x + newtunnering.y * newtunnering.y + newtunnering.z * newtunnering.z;
                    Debug.Log(kyori.ToString());
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
                    originhit = hitRight.point;//視点を代入
                    origindirection = originhit.x * originhit.x + originhit.y * originhit.y + originhit.z * originhit.z;//距離の2乗を計算
                    origindirection2 = Mathf.Sqrt(origindirection);
                    origindirection3 = Mathf.Sqrt(origindirection2);
                    newtunnering = new Vector3(originhit.x * root10 / origindirection3, originhit.y * root10 / origindirection3, originhit.z * root10 / origindirection3);//トンネリングの座標を計算して代入
                    transform.position = newtunnering;//トンネリングを移動
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
                    originhit = hitLeft.point;//視点を代入
                    origindirection = originhit.x * originhit.x + originhit.y * originhit.y + originhit.z * originhit.z;//距離の2乗を計算
                    origindirection2 = Mathf.Sqrt(origindirection);
                    origindirection3 = Mathf.Sqrt(origindirection2);
                    newtunnering = new Vector3(originhit.x * root10 / origindirection3, originhit.y * root10 / origindirection3, originhit.z * root10 / origindirection3);//トンネリングの座標を計算して代入
                    transform.position = newtunnering;//トンネリングを移動
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
