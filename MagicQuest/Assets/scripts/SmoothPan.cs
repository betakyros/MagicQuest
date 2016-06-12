using UnityEngine;
using System.Collections;

public class SmoothPan : MonoBehaviour {

    public Transform[] targets;
    public float linearMoveSpeed;
    public float smoothTime2;
    public GameObject screenFader;

    private Vector3 velocity = Vector3.zero;
    private int currentTargetPointer;
    private Vector3 nextLocation;

    void Start()
    {
        currentTargetPointer = 0;
        nextLocation = new Vector3(targets[currentTargetPointer].position.x, targets[currentTargetPointer].position.y, transform.position.z);
    }

    void Update()
    {
        if (currentTargetPointer != targets.Length)
        {
            if ((new Vector3(transform.position.x, transform.position.y) - new Vector3(targets[currentTargetPointer].position.x, targets[currentTargetPointer].position.y)).magnitude < 1.5f)
            {
                currentTargetPointer++;
                if (currentTargetPointer != targets.Length)
                {
                    nextLocation = new Vector3(targets[currentTargetPointer].position.x, targets[currentTargetPointer].position.y, transform.position.z);
                }
            }
            else
            {
                SmoothPanToLocation();
            }
        }
        else
        {
            screenFader.GetComponent<FadeInOut>().EndScene(2);
        }

    }

    void SmoothPanToLocation()
    {

        if (currentTargetPointer % 2 == 1)
        {
            Vector3 moveDirection = (new Vector3(nextLocation.x, nextLocation.y)- new Vector3(transform.position.x, transform.position.y)).normalized;
            transform.position = transform.position + moveDirection * linearMoveSpeed;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, nextLocation, ref velocity, smoothTime2);
        }
    }
}
