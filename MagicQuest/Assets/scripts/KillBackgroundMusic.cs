using UnityEngine;
using System.Collections;

public class KillBackgroundMusic : MonoBehaviour {

	// Use this for initialization
	void Awake () {
                //disable all background music
            foreach (GameObject backgroundMusic in GameObject.FindGameObjectsWithTag("backgroundMusic"))
            {
                Destroy(backgroundMusic);
            }
    }
}
