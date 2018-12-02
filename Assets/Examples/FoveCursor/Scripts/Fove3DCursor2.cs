using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fove3DCursor2 : MonoBehaviour {
    public Material plane;//マテリアルの変数
    public Vector3 pos;//位置の変数
    public Vector3 eyepos;//視点の位置の変数
    public Vector3 tunpos;//トンネリングの位置の変数
    public Vector3 displaysize;

    // Use this for initialization
    void Start () {
        displaysize = new Vector3(25, 14, 1);
		
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
                    eyepos = hitLeft.point + ((hitRight.point - hitLeft.point) / 2);
                    //eyepos = eyepos / displaysize;
                    eyepos.x = eyepos.x / displaysize.x;
                    eyepos.y = eyepos.y / displaysize.y;
                    plane.SetFloat("_UX", eyepos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", eyepos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                else
                {
                    eyepos = eyes.left.GetPoint(3.0f) + ((eyes.right.GetPoint(3.0f) - eyes.left.GetPoint(3.0f)) / 2); ;
                    //eyepos = eyepos / displaysize;
                    eyepos.x = eyepos.x / displaysize.x;
                    eyepos.y = eyepos.y / displaysize.y;
                    plane.SetFloat("_UX", eyepos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", eyepos.y);//マウスのy座標をシェーダーのx座標に代入
                }

                break;
            case Fove.EFVR_Eye.Left:

                Physics.Raycast(eyes.right, out hitRight, Mathf.Infinity);
                if (hitRight.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
                {
                    eyepos = hitRight.point;
                    //eyepos = eyepos / displaysize;
                    eyepos.x = eyepos.x / displaysize.x;
                    eyepos.y = eyepos.y / displaysize.y;
                    plane.SetFloat("_UX", eyepos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", eyepos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                else
                {
                    eyepos = eyes.right.GetPoint(3.0f);
                    //eyepos = eyepos / displaysize;
                    eyepos.x = eyepos.x / displaysize.x;
                    eyepos.y = eyepos.y / displaysize.y;
                    plane.SetFloat("_UX", eyepos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", eyepos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                break;
            case Fove.EFVR_Eye.Right:

                Physics.Raycast(eyes.left, out hitLeft, Mathf.Infinity);
                if (hitLeft.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
                {
                    eyepos = hitLeft.point;
                    //eyepos = eyepos / displaysize;
                    eyepos.x = eyepos.x / displaysize.x;
                    eyepos.y = eyepos.y / displaysize.y;
                    plane.SetFloat("_UX", eyepos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", eyepos.y);//マウスのy座標をシェーダーのx座標に代入
                }
                else
                {
                    eyepos = eyes.left.GetPoint(3.0f);
                    //eyepos = eyepos / displaysize;
                    eyepos.x = eyepos.x / displaysize.x;
                    eyepos.y = eyepos.y / displaysize.y;
                    plane.SetFloat("_UX", eyepos.x);//マウスのx座標をシェーダーのx座標に代入
                    plane.SetFloat("_VY", eyepos.y);//マウスのy座標をシェーダーのx座標に代入
                    
                }
                break;
        }
    }
}
