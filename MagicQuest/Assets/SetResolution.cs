using UnityEngine;
using System.Collections;

public class SetResolution : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Screen.SetResolution(1150, 550, false);
    }
}
