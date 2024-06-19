using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator dialogueAnimator;
    public Animator choicePanelAnimator;
    public Animator testButtonAnimator; // Optional

    public GameObject choicePanel;
    public Button choiceButtonPrefab;

    private Queue<string> sentences;
    private DialogueScriptableObject currentDialogue; // Reference to the current dialogue

    private static DialogueManager _instance; // Singleton pattern

    public static DialogueManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(DialogueManager).Name);
                    _instance = singletonObject.AddComponent<DialogueManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        sentences = new Queue<string>();
    }

    // Create a method to start the conversation
    public void StartDialogue(DialogueScriptableObject dialogueScriptableObject)
    {
        dialogueAnimator.SetBool("IsOpen", true);

        nameText.text = dialogueScriptableObject.characterName;

        // Clear previous sentences before starting the new dialogue
        sentences.Clear();

        // Iterate through each sentence in dialogue
        foreach (string sentence in dialogueScriptableObject.sentences)
        {
            // Add new sentence to the queue
            sentences.Enqueue(sentence);
        }

        currentDialogue = dialogueScriptableObject;
        DisplayNextSentence();
    }

    // Create a method to display the next sentence
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            // Check if there's a next dialogue after finishing the current one
            if (currentDialogue.nextDialogue != null)
            {
                StartDialogue(currentDialogue.nextDialogue); // Automatically progress to next dialogue
            }
            else
            {
                HideDialogueBox();
            }

            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Animate the appearance of the letters
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void ShowChoices()
    {
        choicePanel.SetActive(true);

        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ChoiceScriptableObject choice in currentDialogue.choices)
        {
            Button choiceButton = Instantiate(choiceButtonPrefab, choicePanel.transform);
            TMP_Text buttonText = choiceButton.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
            {
                buttonText.text = choice.choiceText;
            }
            else
            {
                Debug.LogError("TMP_Text component not found in choiceButtonPrefab's children.");
            }

            choiceButton.onClick.AddListener(() => OnChoiceSelected(choice));
        }
    }

    void OnChoiceSelected(ChoiceScriptableObject choice)
    {
        choicePanel.SetActive(false);
        StartDialogue(choice.nextDialogue);
    }

    void HideDialogueBox()
    {
        dialogueAnimator.SetBool("IsOpen", false);
    }
}
