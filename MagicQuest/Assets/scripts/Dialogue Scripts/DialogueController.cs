using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//todo refactor this in a way that reduces duplicated code
public class DialogueController : MonoBehaviour {


    //todo move these into an array?
    public AudioClip orcVoice;
    public AudioClip mainCharacterVoice;

    public Image orcImage;
    public Image mainCharacterImage;

    public Button nextButton;
    public Button doneButton;
    public Button doneButtonOnePointFive;
    public Button skipButtonOnePointFive;
    public Button doneButton2;
    public Button doneButton3;
    public Button yesButton;
    public Button noButton;

    private DialogueText[] dialogueLines;
    private int currentDialoguePointer;
    private ModalPanel modalPanel;
    
    void Start()
    {
        modalPanel = ModalPanel.Instance();

        //todo think about storing these dialogue lines in a formatted text file.
        dialogueLines = new DialogueText[] {
            new DialogueText("Halt! You are entering the realm of Rerec Roslve great sorcerer and the future first ruler of the world! State your name and purpose.",
                orcVoice, orcImage, new Button[] { nextButton }),
            new DialogueText("I am Mei, Rerec has kidnapped the prince of So'maplace and I've come to bring him home. Let me through.",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton }),
            new DialogueText("I have heard of your great power! Come join us. Together, we can serve Rerec and conquer the land!",
                orcVoice, orcImage, new Button[] { nextButton }),
            new DialogueText("What should I do?",
                mainCharacterVoice, mainCharacterImage, new Button[] { yesButton, noButton }),
            new DialogueText("Ok why not? Let's destroy the world! BWAH HA HA HA!",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton }),
            new DialogueText("Great! ....... Well I guess the game is over now",
                orcVoice, orcImage, new Button[] { doneButton2}),
            new DialogueText("No! I must rescue the prince! Get out of my way!",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton }),
            new DialogueText("I’m afraid I can’t do that. Have a taste of my WALL MAGIC!",
                orcVoice, orcImage, new Button[] { doneButton}),
            new DialogueText("His WALL MAGIC is powerful. I must use my ATTACK MAGIC carefully",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton, skipButtonOnePointFive }),
            new DialogueText("I have three locations from which I can launch my magic. To navigate between them I need to press the left or right arrow keys",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton, skipButtonOnePointFive }),
            new DialogueText("If I don't like the magic I have already loaded at that position, I can press the down arrow key to reload.",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton, skipButtonOnePointFive }),
            new DialogueText("To send out my attack magic, I must press the space key.",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton, skipButtonOnePointFive }),
            new DialogueText("My Red magic is strong against his Green magic and weak against his Blue magic. My Blue is strong against Red and weak against Green, and Green is strong against Blue and weak to Red",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton, skipButtonOnePointFive }),
            new DialogueText("How did I remember the ordering? Oh right, Pokemon weaknesses! Fire beats grass, grass beats water, water beats fire.",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton, skipButtonOnePointFive }),
            new DialogueText("I must be careful. If I tie or lose, my magic power will not be refunded.",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton, skipButtonOnePointFive }),
            new DialogueText("If I run out of magical power I'm done for.",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton, skipButtonOnePointFive }),
            new DialogueText("Ok! Here I go.",
                mainCharacterVoice, mainCharacterImage, new Button[] { doneButtonOnePointFive }),
            new DialogueText("Ouch you've beaten me! You may be even stronger than Rerec. Please let me serve you.",
                orcVoice, orcImage, new Button[] { nextButton}),
            new DialogueText("Fine but don't get in my way.",
                mainCharacterVoice, mainCharacterImage, new Button[] { nextButton }),
            new DialogueText("Yata!",
                orcVoice, orcImage, new Button[] { doneButton3}),
        };

        currentDialoguePointer = -1;
        NextFunction();

    }

    public void NextFunction()
    {

        if (modalPanel.getCurrentStringDisplayFinished())
        {
            DisableDisableableDialogue(currentDialoguePointer >= 16);
            currentDialoguePointer += 1;
            DialogueText curr = dialogueLines[currentDialoguePointer];
            modalPanel.DialogueChoice(curr.getDialogueString(), curr.getCurrentSpeakerImage(), curr.getCurrentVoice(), curr.getDialogueResponses());
        }
        else
        {
            modalPanel.setCurrentStringDisplayInterrupted();
        }

    }

    public void YesFunction()
    {
        if (modalPanel.getCurrentStringDisplayFinished())
        {
            DisableDisableableDialogue(true);
            currentDialoguePointer += 1;
            DialogueText curr = dialogueLines[currentDialoguePointer];
            modalPanel.DialogueChoice(curr.getDialogueString(), curr.getCurrentSpeakerImage(), curr.getCurrentVoice(), curr.getDialogueResponses());
        }
        else
        {
            modalPanel.setCurrentStringDisplayInterrupted();
        }
    }

    public void NoFunction()
    {
        if (modalPanel.getCurrentStringDisplayFinished())
        {
            DisableDisableableDialogue(true);
            currentDialoguePointer += 3;
            DialogueText curr = dialogueLines[currentDialoguePointer];
            modalPanel.DialogueChoice(curr.getDialogueString(), curr.getCurrentSpeakerImage(), curr.getCurrentVoice(), curr.getDialogueResponses());
        }
        else
        {
            modalPanel.setCurrentStringDisplayInterrupted();
        }
    }

    public void DoneFunction()
    {
        if (modalPanel.getCurrentStringDisplayFinished())
        {
            DisableDisableableDialogue(true);
            modalPanel.ClosePanel();
            //should 3 not be hardcoded?
            SceneManager.LoadScene(4, LoadSceneMode.Additive);
        }
        else
        {
            modalPanel.setCurrentStringDisplayInterrupted();
        }
    }

    public void SkipFunctionOnePointFive()
    {
        if (modalPanel.getCurrentStringDisplayFinished())
        {
            DisableDisableableDialogue(false);
            modalPanel.ClosePanel();
            currentDialoguePointer = 16;
        }
        else
        {
            modalPanel.setCurrentStringDisplayInterrupted();
        }
    }

    public void DoneFunctionOnePointFive()
    {
        if (modalPanel.getCurrentStringDisplayFinished())
        {
            DisableDisableableDialogue(false);
            modalPanel.ClosePanel();
        }
        else
        {
            modalPanel.setCurrentStringDisplayInterrupted();
        }
    }

    public void DoneFunction2()
    {
        if (modalPanel.getCurrentStringDisplayFinished())
        {
            DisableDisableableDialogue(true);
            modalPanel.ClosePanel();
            //should 3 not be hardcoded?
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
        }
        else
        {
            modalPanel.setCurrentStringDisplayInterrupted();
        }
    }

    public void DoneFunction3()
    {
        if (modalPanel.getCurrentStringDisplayFinished())
        {
            DisableDisableableDialogue(true);
            modalPanel.ClosePanel();
            //should 3 not be hardcoded?
            SceneManager.LoadScene(6, LoadSceneMode.Additive);
        }
        else
        {
            modalPanel.setCurrentStringDisplayInterrupted();
        }
    }

    //the boolean parameter is shitty. todo figure out a better way to get the magic remaining text to stay
    void DisableDisableableDialogue(bool alsoDisableDisableableDialogue2)
    {
        foreach (GameObject comp in GameObject.FindGameObjectsWithTag("disableableDialogue"))
        {
            comp.SetActive(false);
        }
        if(alsoDisableDisableableDialogue2)
        {
            foreach (GameObject comp in GameObject.FindGameObjectsWithTag("disableableDialogue2"))
            {
                comp.SetActive(false);
            }
        }
    }

    internal class DialogueText
    {
        public string dialogueString;
        public AudioClip currentVoice;
        public Image currentSpeakerImage;
        public Button[] dialogueResponses;

        public DialogueText(string dialogueString, AudioClip currentVoice, Image currentSpeakerImage, Button[] dialogueResponses) 
        {
            this.dialogueString = dialogueString;
            this.currentVoice = currentVoice;
            this.dialogueResponses = dialogueResponses;
            this.currentSpeakerImage = currentSpeakerImage;
        }

        public string getDialogueString()
        {
            return dialogueString;
        }

        public AudioClip getCurrentVoice()
        {
            return currentVoice;
        }

        public Image getCurrentSpeakerImage()
        {
            return currentSpeakerImage;
        }

        public Button[] getDialogueResponses()
        {
            return dialogueResponses;
        }
    }
}
