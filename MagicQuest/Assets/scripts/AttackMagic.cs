using UnityEngine;
using System.Collections;

public class AttackMagic : Magic {

    public AudioClip attackReload;
    public AudioClip attackFail;
    public AudioClip[] orcLaughs;

    public void Reload()
    {
        ReturnToPool();
    }

    new public void DestroyMagic(Vector3 moveDirection)
    {
        AudioSource.PlayClipAtPoint(attackFail, Camera.main.transform.position);
        base.DestroyMagic(moveDirection);                    
        
        //25% of the time play a laugh clip
        if (Random.Range(0, 4) == 3)
        {
            PlayRandomOrcLaugh();
        }
    }

    //todo make this go back into your ammo pool
    void ReturnToPool()
    {
        //disable collider while returning to pool
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            Destroy(c);
        }
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        StartCoroutine(FlashAndReturnToAmmoPool());
    }

    IEnumerator FlashAndReturnToAmmoPool()
    {
        AudioSource.PlayClipAtPoint(attackReload, Camera.main.transform.position);
        float currentAlpha = 1f;
        //one cycle of on and off
        float flashTime = 0.2f;
        for (float f = .6f; f >= 0; f -= 0.01f)
        {
            if (f % flashTime > flashTime / 2.0f)
            {
                currentAlpha -= .1f;
            }
            else
            {
                currentAlpha += .1f;
            }
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, currentAlpha);
            yield return new WaitForSeconds(.01f);
        }
        StartCoroutine(ReturnToAmmoPool());
    }

    IEnumerator ReturnToAmmoPool()
    {

        Vector3 moveDirection = (initialPosition - GetComponent<Transform>().position).normalized;
        //should the framerate be defined like this?
        for (float f = 3f; f >= 0; f -= 0.01f)
        {
            //todo think about .1 as a "close enough" value
            if ((initialPosition - GetComponent<Transform>().position).magnitude < .3)
            {
                break;
            }
            transform.position = transform.position + moveDirection * deathMoveSpeed * Time.deltaTime;
            yield return new WaitForSeconds(.01f);
        }
        GameObject.Find("MagicLauncherController").GetComponent<MagicLauncherController>().IncrementMagic();
        Destroy(this.gameObject);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        Magic otherProperties = coll.gameObject.GetComponent<Magic>();
        if (otherProperties != null)
        {
            if (myType == MagicProperties.MagicType.ATTACK && otherProperties.myType == MagicProperties.MagicType.TARGET)
            {
                if (doIWin(this, otherProperties))
                {
                    otherProperties.DestroyMagic(coll.gameObject.transform.position - this.gameObject.transform.position);
                    ReturnToPool();
                }
                else if (doITie(this, otherProperties))
                {
                    DestroyMagic(this.gameObject.transform.position - coll.gameObject.transform.position);
                    otherProperties.DestroyMagic(coll.gameObject.transform.position - this.gameObject.transform.position);
                }
                else
                {
                    DestroyMagic(this.gameObject.transform.position - coll.gameObject.transform.position);
                }
            }
        }
    }

    //this doesnt belong here. but im lazy
    public void PlayRandomOrcLaugh()
    {
        int audioIndex = Random.Range(0, orcLaughs.Length);
        AudioSource.PlayClipAtPoint(orcLaughs[audioIndex], Camera.main.transform.position);
    }
}
