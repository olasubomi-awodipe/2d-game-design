using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choice", menuName = "Choice")]
public class ChoiceScriptableObject : ScriptableObject
{
    public string choiceText;
    public DialogueScriptableObject nextDialogue;
}
