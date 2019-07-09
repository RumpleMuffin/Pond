using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {

	public GameObject tile;
	public Material water;
	

	public float rotate = 2;
	float scale = 10;
	Vector3 offset = new Vector3(0.5f, 0.5f, 0);
	Vector3 tiling = new Vector3(1, 1, 1);

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		rotate += Time.deltaTime * scale;


		Quaternion quat = Quaternion.Euler(0, 0, rotate);
		Matrix4x4 matrix1 = Matrix4x4.TRS(offset, Quaternion.identity, tiling);
		Matrix4x4 matrix2 = Matrix4x4.TRS(Vector3.zero, quat, tiling);
		Matrix4x4 matrix3 = Matrix4x4.TRS(-offset, Quaternion.identity, tiling);
		water.SetMatrix("_Matrix", matrix1 * matrix2 * matrix3);

	}
}
