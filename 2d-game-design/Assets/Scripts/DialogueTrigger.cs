using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // create an object of the dialogue class
    public DialogueScriptableObject dialogue;

    // create a method to begin the dialogue
    public void TriggerDialogue ()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
