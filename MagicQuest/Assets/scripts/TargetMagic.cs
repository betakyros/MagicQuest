using UnityEngine;
using System.Collections;

public class TargetMagic : Magic {

    void OnCollisionEnter2D(Collision2D coll)
    {
        Magic otherProperties = coll.gameObject.GetComponent<Magic>();
        if (otherProperties != null)
        {
            //if they are two tagets, don't let them interact anymore
            if (myType == MagicProperties.MagicType.TARGET && otherProperties.myType == MagicProperties.MagicType.TARGET)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), coll.collider);
            }
        }
    }
}
