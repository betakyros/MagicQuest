using UnityEngine;
using System.Collections;

public class PushAnyButtonToContinue : MonoBehaviour {
    
    public Canvas canvas;
    public int nextState;

	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            canvas.GetComponent<LoadOnClick>().LoadScene(nextState);
        }
    }
}
