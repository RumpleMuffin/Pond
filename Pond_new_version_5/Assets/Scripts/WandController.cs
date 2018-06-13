using UnityEngine;
using System.Collections;

public class WandController : MonoBehaviour {

    public GameObject Grp_Scripts;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
	private DecayBehavior decayBehaviour;

    float startPos, currentPos, startDecay;
    
    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
		decayBehaviour = Grp_Scripts.GetComponent<DecayBehavior>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (controller == null) {
			Debug.Log("Controller not initialized");
            return;
        }

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        /*
        if (gripButtonUp) {
            Debug.Log("Grip Button was just unpressed");
        }
        if (triggerButtonUp) {
            Debug.Log("Trigger Button was just unpressed");
        }
        */

        if (gripButtonDown)
        {
            //Grp_Scripts.GetComponent<DecayBehavior>().Decay = 1;
            //Debug.Log("Grip Button was just pressed");
        }

        if (triggerButtonDown)
        {
            startPos = trackedObj.gameObject.transform.position.y;
            startDecay = decayBehaviour.GetDecay();

            //Debug.Log("Trigger Button was just pressed");
        }

        if (triggerButtonPressed)
        {
            currentPos = trackedObj.gameObject.transform.position.y;

			//sets the decay value in decay behaviour
            decayBehaviour.SetDecay(startDecay + ((startPos - currentPos)*1.9f));
        }	
    }
}
