using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public string characterName;
    [TextArea(3, 10)]

    public string[] sentences;
    // public ChoiceScriptableObject[] choices;

    public ChoiceScriptableObject[] choices;
    public DialogueScriptableObject nextDialogue;
}



