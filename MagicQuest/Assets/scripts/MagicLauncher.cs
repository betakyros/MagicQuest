using System.Collections;
using UnityEngine;

public class MagicLauncher : MonoBehaviour {

    public GameObject[] magics;
    public MagicLauncherController magicLauncherController;
    public AudioClip launchSound;
    private bool isLoaded;
    private GameObject myCurrentMagic;
    //the magic launcher is out of ammo if it has already failed to reload once
    private bool outOfAmmo;

    // Use this for initialization
    void Start()
    {
        outOfAmmo = false;
        isLoaded = true;
        ReloadMagic();
    }

    void Update()
    {
        if (!isLoaded && myCurrentMagic == null && !outOfAmmo)
        {
            ReloadMagic();
        }
    }

    //returns true if launched and false if no launch happens
    public bool Launch()
    {
        if (isLoaded)
        {
            isLoaded = false;
            AudioSource.PlayClipAtPoint(launchSound, Camera.main.transform.position);
            myCurrentMagic.GetComponent<Rigidbody2D>().AddForce((int)MagicProperties.MoveSpeed.ATTACK_MOVE_SPEED * Vector3.up);
            return true;
        }
        return false;
    }

    public void Reload()
    {
        if (myCurrentMagic != null && isLoaded && !outOfAmmo)
        {
            isLoaded = false;
            //think about this name. Maybe it should be animateReload();
            myCurrentMagic.GetComponent<AttackMagic>().Reload();
            StartCoroutine(WaitUntilMagicIsDestroyedThenReload());
        }
    }

    public bool IsOutOfAmmo()
    {
        return outOfAmmo;
    }

    //todo is there a better way to perform functions serially?
    IEnumerator WaitUntilMagicIsDestroyedThenReload()
    {
        while (myCurrentMagic != null)
        {
            yield return new WaitForSeconds(.1f);
        }
        ReloadMagic();
    }

    void ReloadMagic()
    {
        //todo play reload animation
        if (magicLauncherController.magicRemaining > 0 && !outOfAmmo)
        {
            isLoaded = true;
            magicLauncherController.DecrementMagic();
            myCurrentMagic = (GameObject)Instantiate(magics[Random.Range(0, magics.Length)], gameObject.transform.position, new Quaternion());
        }
        else
        {
            outOfAmmo = true;
            magicLauncherController.CheckIfILost();
            //todo play reload fail sound
        }
    }
}
