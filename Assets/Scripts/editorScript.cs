using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class editorScript : MonoBehaviour {

    private float decayControl;
	public float decay;
	public bool canDecay;
	public float decayTime;
    private DecayBehavior decayBehaviour;

    private void Start()
    {
        decayBehaviour = GetComponent<DecayBehavior>();
		StartCoroutine(StartDecay());
    }
    private void Update()
    {
        if(decay <= .55f && canDecay == true)
        {
			decay = decay + decayTime * Time.deltaTime;
            decayBehaviour.SetDecay(0);
            
        }

	}

	IEnumerator StartDecay()
	{
		yield return new WaitForSeconds(1);
		//this.enabled = false;
		canDecay = true;
		
	}
}
