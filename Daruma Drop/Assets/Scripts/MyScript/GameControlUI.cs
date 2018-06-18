using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlUI : MonoBehaviour {

    public int rectCount = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.G))
        {
            EventBroadcaster.Instance.PostEvent("ON_CLICK_RED_EVENT");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            EventBroadcaster.Instance.PostEvent("ON_CLICK_GREEN_EVENT");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            EventBroadcaster.Instance.PostEvent("ON_CLICK_BLUE_EVENT");
        }
    }

    public void OnStartClick()
    {
        Parameters param = new Parameters();
        param.PutExtra("NUM_BOX_KEY", rectCount);
        EventBroadcaster.Instance.PostEvent("ON_START_EVENT", param);
    }

    public void OnRedClick()
    {
        EventBroadcaster.Instance.PostEvent("ON_CLICK_RED_EVENT");
    }
    public void OnGreenClick()
    {
        EventBroadcaster.Instance.PostEvent("ON_CLICK_GREEN_EVENT");
    }
    public void OnBlueClick()
    {
        EventBroadcaster.Instance.PostEvent("ON_CLICK_BLUE_EVENT");
    }
}
