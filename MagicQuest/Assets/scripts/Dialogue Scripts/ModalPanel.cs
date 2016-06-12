using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanel : MonoBehaviour {

    public GameObject modalPanelObject;
    public Text currentText;

    private float letterPause = 0.015f;
    private string currentString;
    private AudioClip currentVoice;
    private AudioSource audioSource;
    private static ModalPanel modalPanel;
    private bool currentStringDisplayFinished;
    private bool currentStringDisplayInterrupted;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentStringDisplayFinished = true;
        currentStringDisplayInterrupted = false;
    }

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
            {
                Debug.LogError("modal panel doesnt exist");
            }
        }
        return modalPanel;
    }
    
    //todo think about character image location
    public void DialogueChoice(string newPanelText, Image newSpeakerImage, AudioClip newSpeakerVoice, Button[] buttons)
    {
        modalPanelObject.gameObject.SetActive(true);
        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
        newSpeakerImage.gameObject.SetActive(true);
        currentVoice = newSpeakerVoice;
        currentString = newPanelText;
        StartCoroutine(TypeText());
    }

    public void ClosePanel () 
    {
        modalPanelObject.SetActive(false);
    }

    public bool getCurrentStringDisplayFinished()
    {
        return currentStringDisplayFinished;
    }

    public void setCurrentStringDisplayInterrupted()
    {
        currentStringDisplayInterrupted = true;
    }

    IEnumerator TypeText()
    {
        currentStringDisplayFinished = false;
        currentStringDisplayInterrupted = false;

        currentText.text = "";
        foreach (char letter in currentString.ToCharArray())
        {
            if (currentStringDisplayInterrupted)
            {
                currentText.text = currentString;
                break;
            }

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(currentVoice);
            }

            currentText.text += letter;
            if (letter.CompareTo('.') == 0 || letter.CompareTo('!') == 0)
            {
                yield return new WaitForSeconds(letterPause * 20);

            }
            else
            {
                yield return new WaitForSeconds(letterPause);
            }
        }
        currentStringDisplayFinished = true;
    }

    
}
