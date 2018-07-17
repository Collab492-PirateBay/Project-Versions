/* DIALOGUE */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue 
{
    //public Material npcAvatarImage;

    //public Sprite npcAvatarSprite;

    public string npcName;

    //DIALOGUE GROUPS / SENTENCES
    //Text Area gives you more space for the Inspector, so you can write longer dialogues
    [TextArea(3, 10)]
    public string[] dialogueGroup;

}
