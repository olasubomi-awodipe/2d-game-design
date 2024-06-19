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
    public Animator testButtonAnimator;

    public GameObject choicePanel;
    public Button choiceButtonPrefab;

    private Queue<string> sentences;
    private DialogueScriptableObject currentDialogue; //

    private static DialogueManager _instance; //
    public static DialogueManager Instance //
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

    private void Awake() //
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


    // create a method to start the conversation
    public  void StartDialogue (DialogueScriptableObject dialogueScriptableObject)
    {
        dialogueAnimator.SetBool("IsOpen", true);
        

        nameText.text = dialogueScriptableObject.characterName;

        // clear the previous sentence before the start of the new sentence
        sentences.Clear();

        // iterate through each sentence in dialogue
        foreach (string sentence in dialogueScriptableObject.sentences)
        {
            // add a new sente
            sentences.Enqueue(sentence);
           
        }

        currentDialogue = dialogueScriptableObject;
        DisplayNextSentence();
        
    }

    // create a method to display the next sentence
    public void DisplayNextSentence ()
    {
        
        // check if there is any sentence left in the current dialogue
        if (sentences.Count == 0)
        {
            if (currentDialogue.choices.Length > 0)
            {
                ShowChoices();
            }
           
            else
            { 
                HideDialogueBox();
            }
            
            return;
        }

         
        string sentence = sentences.Dequeue();
        // stops sentence animation before a new sentence begins
        StopAllCoroutines();
        // calls the coroutine for TypeSentence
        StartCoroutine(TypeSentence(sentence));  
    }

    // to animate the appearance of the letters
    IEnumerator TypeSentence (string sentence)
    {
        // create an empty string
        dialogueText.text = "";

        // iterate through each letter in the sentence
        // ToCharArray converts a string to a character array
        foreach (char letter in sentence.ToCharArray())
        {
            // appends each letter to the end of the string
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void HideChoicePanel()
    {
        if (currentDialogue.choices == null)
        {
            choicePanelAnimator.SetBool("IsOpen", false);
        }
    }
    void ShowChoicePanel()
    {
        if (currentDialogue.choices != null)
        {
            choicePanelAnimator.SetBool("IsOpen", true);
        }
    }

    void HideTestButton()
    {
        testButtonAnimator.SetBool("IsOpen", false);
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
            Debug.Log("The choice prefab exist");
            // choiceButton.GetComponentInChildren<Text>().text = choice.choiceText;
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
