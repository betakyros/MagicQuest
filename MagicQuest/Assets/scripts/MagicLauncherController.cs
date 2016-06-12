using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MagicLauncherController : MonoBehaviour {

    public MagicLauncher[] magicLaunchers;
    public GameObject player;
    public Text magicRemainingText;
    private int selectedIndex;
    public int magicRemaining;

	// Use this for initialization
	void Start () {
        selectedIndex = 0;
        player = (GameObject)Instantiate(player);
        UpdateSelectAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            magicLaunchers[selectedIndex].Launch();
        }
        else if (Input.GetKeyDown("left"))
        {
            int numMagicLaunchers = magicLaunchers.Length;
            selectedIndex = (selectedIndex + numMagicLaunchers - 1) % numMagicLaunchers;
            UpdateSelectAnimation();
        }
        else if (Input.GetKeyDown("right"))
        {
            int numMagicLaunchers = magicLaunchers.Length;
            selectedIndex = (selectedIndex + 1) % numMagicLaunchers;
            UpdateSelectAnimation();
        }
        else if (Input.GetKeyDown("down"))
        {
            magicLaunchers[selectedIndex].Reload();
        }

    }

    void UpdateSelectAnimation()
    {
        Vector3 behindLauncher = new Vector3(magicLaunchers[selectedIndex].transform.position.x, magicLaunchers[selectedIndex].transform.position.y - 1);
        player.transform.position = behindLauncher; 
    }

    public void DecrementMagic()
    {
        magicRemaining--;
        UpdateMagicRemainingText();
    }

    public void IncrementMagic()
    {
        magicRemaining++;
        UpdateMagicRemainingText();
    }

    public void CheckIfILost()
    {
        bool didILose = true;
        foreach (MagicLauncher magicLauncher in magicLaunchers)
        {
            didILose &= magicLauncher.IsOutOfAmmo();
        }

        if(didILose)
        {
            SceneManager.LoadScene(7);
        }

    }

    void UpdateMagicRemainingText()
    {
        magicRemainingText.text = "Magic Remaining: " + magicRemaining;
    }
}
