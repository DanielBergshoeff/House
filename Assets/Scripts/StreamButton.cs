using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StreamButton : MonoBehaviour
{
    public Comment defaultComment;
    public Comment emptyComment;
    public MinigameManager minigameManager;

    // Start is called before the first frame update
    void Awake()
    {
        //GetComponent<MinigameManager>(); //reference so you can steal all info in that script
        //EmptyComment(); //wipe all comment data from the beginning by replacing it with a clean one
    }

    void SetColor(TextMeshProUGUI commentContainer)
    {
        if (defaultComment.nature == Comment.Nature.Bad || defaultComment.nature == Comment.Nature.Ruminate)
        { //if current comment is negative
            commentContainer.color = minigameManager.negativeColor;
        }

        if (defaultComment.nature == Comment.Nature.Good || defaultComment.nature == Comment.Nature.Counter)
        {
            commentContainer.color = minigameManager.positiveColor;
        }

        if (defaultComment.nature == Comment.Nature.Neutral)
        {
            commentContainer.color = minigameManager.neutralColor;
        }
    }

    public void SetComment(Comment otherComment) {
        Debug.Log("SetComment() has been called");
        defaultComment.speaker = otherComment.speaker;
        defaultComment.Phase = otherComment.Phase;
        defaultComment.nature = otherComment.nature;
        defaultComment.Text = otherComment.Text;

        TextMeshProUGUI commentContainer = gameObject.GetComponentInChildren<TextMeshProUGUI>(); //declare the place where text is set
        SetColor(commentContainer);
    }

    public void EmptyComment() {
        defaultComment.speaker = emptyComment.speaker;
        defaultComment.Phase = emptyComment.Phase;
        defaultComment.nature = emptyComment.nature;
        defaultComment.Text = emptyComment.Text;
    }

    // Ban()
    // Highlight()
    //here or in minigame
}
