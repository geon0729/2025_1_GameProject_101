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
            Debug.LogError("���̾� �α� �Ŵ����� �����ϴ�.");
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
