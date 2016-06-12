using System.Collections;
using UnityEngine;

//consider splitting magic into attack and target magics
public abstract class Magic : MonoBehaviour {
    
    public MagicProperties.MagicColorName myColor;
    public MagicProperties.MagicType myType;
    protected int deathMoveSpeed;
    protected Vector3 initialPosition;

    void Start()
    {
        deathMoveSpeed = (int)MagicProperties.MoveSpeed.DEATH_MOVE_SPEED;
        initialPosition = GetComponent<Transform>().position;
    }
    //its weird that the magicProperties destroys its parent object. is this normal?
    //maybe call this destroyTarget
    public void DestroyMagic(Vector3 moveDirection)
    {
        if (myType == MagicProperties.MagicType.TARGET)
        {
            GameObject.Find("TargetSpawner").GetComponent<TargetController>().DecrementTargets();
        }

        //disable collider while spinning
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            Destroy(c);
        }
        StartCoroutine(SpinToDeath(moveDirection));
    }


    protected IEnumerator SpinToDeath(Vector3 moveDirection)
    {
        Vector3 randomizedMoveDirection = Quaternion.AngleAxis(Random.Range(-45.0f, 45.0f), Vector3.forward) * moveDirection;
        //should the framerate be defined like this?
        for (float f = 3f; f >= 0; f -= 0.01f)
        {
            transform.Rotate(Vector3.forward * -5);
            transform.position = transform.position + randomizedMoveDirection * deathMoveSpeed * Time.deltaTime;
            yield return new WaitForSeconds(.01f);
        }

        Destroy(this.gameObject);
    }

    /**
     * returns true if the attacker is an attacker, defender is a defender and the attacker wins against the defender
     **/
    protected bool doIWin(Magic attacker, Magic defender)
    {
        if(attacker.myType != MagicProperties.MagicType.ATTACK || defender.myType != MagicProperties.MagicType.TARGET)
        {
            return false;
        }

        int numColors = System.Enum.GetNames(typeof(MagicProperties.MagicColorName)).Length;

        if ((attacker.myColor - defender.myColor + numColors) % numColors == 1)
        {
            return true;
        }

        return false;
    }

    /**
 * returns true if the attacker is an attacker, defender is a defender and the attacker is the same as the defender
 **/
    protected bool doITie(Magic attacker, Magic defender)
    {
        if (attacker.myType != MagicProperties.MagicType.ATTACK || defender.myType != MagicProperties.MagicType.TARGET)
        {
            return false;
        }

        if (attacker.myColor == defender.myColor)
        {
            return true;
        }

        return false;
    }

}
