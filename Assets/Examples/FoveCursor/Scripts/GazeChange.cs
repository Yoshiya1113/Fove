using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeChange : MonoBehaviour {
    Collider mCollider;
    Light mLight;
    public Material[] _material;

    // Use this for initialization
    void Start () {
        mCollider = GetComponent<Collider>();
        mLight = GetComponentInChildren<Light>();

        if (mCollider == null)
            mCollider = gameObject.AddComponent<SphereCollider>();//球の当たり判定

    }
	
	// Update is called once per frame
	void Update () {
        if (FoveInterface.IsLookingAtCollider(mCollider))
        {
            Debug.Log("COLLISION");
            this.GetComponent<Renderer>().material = _material[1];
        }
        else
        {
            this.GetComponent<Renderer>().material = _material[0];
        }

    }
}
