using UnityEngine;
using System.Collections;

public class OutOfBoundsScript : MonoBehaviour {

    // bad duplicated code but im lazy
    public AudioClip[] orcLaughs;
    bool seen = false;

    void Update()
    {
        if (GetComponent<Renderer>().isVisible)
            seen = true;

        if (seen && !GetComponent<Renderer>().isVisible)
        {
            if (Random.Range(0, 8) == 3)
            {
                PlayRandomOrcLaugh();
            }
            Destroy(gameObject);
        }
    }

    //this doesnt belong here. but im lazy
    void PlayRandomOrcLaugh()
    {
        if (orcLaughs.Length > 0)
        {
            int audioIndex = Random.Range(0, orcLaughs.Length);
            AudioSource.PlayClipAtPoint(orcLaughs[audioIndex], Camera.main.transform.position);
        }
    }
}
