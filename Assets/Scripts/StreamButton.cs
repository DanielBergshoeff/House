using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//this script sits on the parent that holds the buttons containing text, aka yellows comments during stream

public class StreamButton : MonoBehaviour
{
    public Comment defaultComment1;
    public Comment defaultComment2;
    public Comment emptyComment;
    public MinigameManager minigameManager;

    // Start is called before the first frame update
    void Awake()
    {
        //GetComponent<MinigameManager>(); //reference so you can steal all info in that script
        //EmptyComment(); //wipe all comment data from the beginning by replacing it with a clean one
    }

    private void Update()
    {
      
            //im never setting the comments to the text components
    }

    void SetColor(TextMeshProUGUI commentContainer, Comment aDefaultComment)
    {
        //color comment
        if (aDefaultComment.nature == Comment.Nature.Bad || defaultComment1.nature == Comment.Nature.Ruminate)
        { //if current comment is negative
            commentContainer.color = minigameManager.negativeColor;
        }

        if (aDefaultComment.nature == Comment.Nature.Good || defaultComment1.nature == Comment.Nature.Counter)
        {
            commentContainer.color = minigameManager.positiveColor;
        }

        if (aDefaultComment.nature == Comment.Nature.Neutral)
        {
            //commentContainer.color = minigameManager.neutralColor;
        }
    }

    public void SetComment1(Comment otherComment) {
        Debug.Log("SetComment() has been called: " + otherComment.speaker + " phase " + otherComment.Phase + " text: " + otherComment.Text);
        defaultComment1.speaker = otherComment.speaker;
        defaultComment1.Phase = otherComment.Phase;
        defaultComment1.nature = otherComment.nature;
        defaultComment1.Text = otherComment.Text;

        TextMeshProUGUI commentContainer = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); //declare the place where text is set
        SetColor(commentContainer, defaultComment1);
        commentContainer.GetComponent<TextMeshProUGUI>().text = defaultComment1.Text;//set text
    }

    public void SetComment2(Comment otherComment)
    {
        Debug.Log("SetComment() has been called: " + otherComment.speaker + " phase " + otherComment.Phase + " text: " + otherComment.Text);
        defaultComment2.speaker = otherComment.speaker;
        defaultComment2.Phase = otherComment.Phase;
        defaultComment2.nature = otherComment.nature;
        defaultComment2.Text = otherComment.Text;

        TextMeshProUGUI commentContainer = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>(); //declare the place where text is set
        SetColor(commentContainer, defaultComment2);
        commentContainer.GetComponent<TextMeshProUGUI>().text = defaultComment2.Text;//set text
    }

    public void EmptyComments() {
        defaultComment1.speaker = emptyComment.speaker;
        defaultComment1.Phase = emptyComment.Phase;
        defaultComment1.nature = emptyComment.nature;
        defaultComment1.Text = emptyComment.Text;

        defaultComment2.speaker = emptyComment.speaker;
        defaultComment2.Phase = emptyComment.Phase;
        defaultComment2.nature = emptyComment.nature;
        defaultComment2.Text = emptyComment.Text;
    }

    // Ban()
    // Highlight()
    //here or in minigame
}
