using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    //
    public TMP_Text nameText;

    public TMP_Text dialogueText;

    private Queue<string> sentences;
    // Use this for initialization
   void Start ()
    {
        // initialize the queue storing the sentences
        sentences = new Queue<string>();
    }

    // create a method to start the conversation
    public  void StartDialogue (Dialogue dialogue)
    {
        // 
        nameText.text = dialogue.name;

        // clear the previous sentence before the start of the new sentence
        sentences.Clear();

        // iterate through each sentence in dialogue
        foreach (string sentence in dialogue.sentences)
        {
            // add a new sente
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // create a method to display the next sentence
    public void DisplayNextSentence ()
    {
        // check if there is any sentence left in the current dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // 
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;  

        void EndDialogue ()
        {
            Debug.Log("End of conversation.");
        }
    }
}
