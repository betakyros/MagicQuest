using UnityEngine;
using System.Collections;

public class BringToFrontMine : MonoBehaviour {

	void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}
