using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public List<Dialogue> dialogues = new List<Dialogue>();

    Queue<string> sentences;
    Dialogue newDialogue;
    int index;

    public bool inPlaying;
    public GameObject panelDialogue;

    [SerializeField] TMP_Text textBox;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void ChangeStateBoxDialogue()
    {
        if (!panelDialogue.activeInHierarchy)
        {
            panelDialogue.SetActive(true);
        }
    }

    public void StartNewDialogue(int index)
    {
        newDialogue = dialogues[index];
        ChangeStateBoxDialogue();
        StartDialogue();
        inPlaying = true;
    }

    public void StartDialogue()
    {
        sentences.Clear();

        foreach (string sentence in newDialogue.sequences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        index++;

        ChangeStateBoxDialogue();
        

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        textBox.text = "";
        yield return new WaitForSeconds(0.2f);
        
        foreach (char letter in sentence.ToCharArray())
        {
            textBox.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        panelDialogue.SetActive(false);
        index = 0;
        inPlaying = false;
        sentences.Clear();
        panelDialogue.SetActive(false);
        newDialogue = null;
    }
}