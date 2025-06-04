using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueDataSO myDialogue;
    public DialogueManager dialogueManger;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManger = FindObjectOfType<DialogueManager>();

        if(dialogueManger == null )
        {
            Debug.LogError("다이얼 로그 매니저가 없습니다.");
        }
    }

    void OnMouseDown()
    {
        if (dialogueManger == null) return;
        if (dialogueManger.IsDialogueActive()) return;
        if (myDialogue == null) return;

        dialogueManger.StartDialogue(myDialogue);

    }
}
