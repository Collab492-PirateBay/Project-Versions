/* TYPEWRITER ANIMATION FOR TEXT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterAnimation : MonoBehaviour 
{
    //TYPEWRITER ANIMATION STYLE
    public float m_TextDelay = 0.0f;
    //public string m_FullText;
    //private string m_CurrentText = "";

    //DIALOGUE QUEUE
    private Queue<string> dialogueGroup;

    //public Renderer npcAvatarImageRenderer;
    //public Material npcAvatarImageMaterial;

    //public Image npcImageSlot;
    //public Sprite npcAvatarSprite;

    public Text npcNameText;
    public Text dialogueText;

    public Animator animator;

    public bool m_IsAbleToContinue;
    public bool m_DialogueIsDone = false;


	private void Start () 
    {
        dialogueGroup = new Queue<string>();
        
        //npcAvatarImageRenderer = GetComponent<Renderer>();
        //npcAvatarImageRenderer.sharedMaterial = npcAvatarImageMaterial;

        //npcImageSlot = GetComponent<Image>();

        //m_IsAbleToContinue = false;

	}

    //COROUTINE FOR TYPEWRITER EFFECT
    IEnumerator VisibleText(string sentence)
    {
        //BELOW FOR JUST HAVING TYPEWRITER EFFECT
        //for (int i = 0; i < m_FullText.Length; i++)
        //{
        //    m_CurrentText = m_FullText.Substring(0, i);
        //    this.GetComponent<Text>().text = m_CurrentText;
        //    yield return new WaitForSeconds(m_TextDelay);
        //}

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            yield return null;
        }
    }

    //BEGIN DIALOGUE BOX
    public void BeginDialogue(Dialogue dialogue)
    {
        dialogueGroup = new Queue<string>();
        animator.SetBool("IsOpen", true);

        //npcAvatarImageMaterial = dialogue.npcAvatarImage;
        //npcImageSlot.sprite = dialogue.npcAvatarSprite;

        npcNameText.text = dialogue.npcName;

        dialogueGroup.Clear();

        foreach (string sentence in dialogue.dialogueGroup)
        {
            dialogueGroup.Enqueue(sentence);
        }
 
        DisplayNextDialogueGroup();
    }

    //MOVE ON TO NEXT DIALOGUE BOX
    public void DisplayNextDialogueGroup()
    {
        Debug.Log(dialogueGroup.Count);
        if (dialogueGroup.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogueGroup.Dequeue();
        //the below stops the typewriter text from typing the next dialogue box over the present one
        StopAllCoroutines();
        //dialogueText.text = sentence;  //REPLACE THIS WITH THE COROUTINE BELOW FOR THE TYPEWRITER ANIMATION
        StartCoroutine(VisibleText(sentence));
    }

    //END THE DIALOGUE BOX
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        m_DialogueIsDone = true;
    }


	private void Update()
	{
        Debug.Log(dialogueGroup.Count);
    }
}
