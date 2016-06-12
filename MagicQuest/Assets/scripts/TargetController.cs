using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour {

    public Text targetsRemainingText;
    private int targetsRemaining;
    private int waveNumber;
    public SpawnTargets targetSpawner;
    private DialogueController dialogueController;

    void Start()
    {
        waveNumber = 0;
        targetsRemaining = 0;

        UpdateTargetRemainingText();
        startNextWave();
        dialogueController = FindObjectOfType(typeof(DialogueController)) as DialogueController;
        dialogueController.NextFunction();
    }

    public void DecrementTargets()
    {
        targetsRemaining--;
        UpdateTargetRemainingText();
        if(targetsRemaining == 0)
        {
            startNextWave();
        }
    }

    public void IncrementTargets()
    {
        targetsRemaining++;
        UpdateTargetRemainingText();
    }

    private void startNextWave()
    {
        if (waveNumber == 0)
        {
            targetSpawner.setTargetMagicsBounds(0, 2);
            targetSpawner.setSpawnRate(1.0f);
            targetSpawner.SendWave(7);
        }
        else if (waveNumber == 1)
        {
            targetSpawner.setSpawnRate(.45f);
            targetSpawner.setTargetMagicsBounds(3, 5);
            targetSpawner.SendWave(8);
        }
        else if (waveNumber == 2)
        {
            targetSpawner.setTargetMagicsBounds(6, 8);
            targetSpawner.SendWave(8);
        }
        else if (waveNumber == 3)
        {
            targetSpawner.setTargetMagicsBounds(9, 14);
            targetSpawner.SendWave(8);
        }
        else
        {
            PlayVictoryAnimation();
        }

        waveNumber++;
    }

    void PlayVictoryAnimation()
    {
        dialogueController.NextFunction();
    }

    void UpdateTargetRemainingText()
    {
        targetsRemainingText.text = "Targets Remaining: " + targetsRemaining.ToString();
    }
}
