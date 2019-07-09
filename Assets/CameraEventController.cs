using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEventController : MonoBehaviour {

	public CinemachineDollyCart dollyCart1;
	public CinemachineDollyCart dollyCart2;
	public CinemachineDollyCart dollyCart3;

	public void DollyCartSpeed1()
	{
		dollyCart1.m_Speed = 2.5f;
	}
	public void DollyCartSpeed2()
	{
		dollyCart2.m_Speed = 2.5f;
	}
	public void DollyCartSpeed3()
	{
		dollyCart3.m_Speed = 2.5f;
	}
}
