/* DIALOGUE TRIGGER */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour 
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<TypeWriterAnimation>().BeginDialogue(dialogue);
    }

    //use this method in other scripts' triggers, or for buttons, in Inpsector
    //go to OnCLick + and add this dialoguetrigger script to it and then click the drop-down
    //and select the triggerdialogue function. Be sure to drag the script from the inspector
    //of the button/object that is the trigger, instead of the plain script from the Project
}
