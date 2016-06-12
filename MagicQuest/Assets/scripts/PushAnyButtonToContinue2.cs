using UnityEngine;
using System.Collections;

//bad name
public class PushAnyButtonToContinue2 : MonoBehaviour {
    
    public GameObject fadeInOut;
    private bool skipped;

    void Start()
    {
        skipped = false;
    }

	// Update is called once per frame
	void Update () {
        if (skipped)
        {
            fadeInOut.GetComponent<FadeInOut>().fadeSpeed *= 2; 
            fadeInOut.GetComponent<FadeInOut>().EndScene(2);
        }
        else if(Input.anyKey)
        {
            skipped = true;
        }
    }
}
