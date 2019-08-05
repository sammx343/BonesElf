using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogsController : MonoBehaviour {
    public Image characterImage;
    public GameObject character; //Character affected by the dialogs

    public int conversationId;

    public GameObject UIpanel;
    public Text textDialog;
    public Button nextButton;
    public Button closeButton;

    public bool isDestroyable;

    private int dialogPosition = 0;
    private Conversation conversation;

    private bool isWrittingText = false;

    // Use this for initialization
    void Start ()
    {
        GameConversations gc = new GameConversations();
        conversation = gc.GetConversationById(conversationId);
    }

    public void NextDialog() {
        if(isWrittingText)
        {
            StopAllCoroutines();
            Dialog currentDialog = conversation.GetDialogByPosition(dialogPosition);
            textDialog.text = currentDialog.GetDialog();
            isWrittingText = false;
        }
        else
        {
            if (dialogPosition == conversation.GetDialogs().Count - 1)
            {
                SwitchUIState(false);
                dialogPosition = 0;
                DestroyDialog();
            }
            else
            {
                dialogPosition++;
                ChangeUI();
            }
        }
    }

    public void CloseDialog()
    {
        StopAllCoroutines();
        SwitchUIState(false);
        dialogPosition = 0;
        DestroyDialog();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Elf")
        {
            SwitchUIState(true);

            nextButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();

            nextButton.onClick.AddListener(delegate { NextDialog(); });

            closeButton.onClick.AddListener(delegate { CloseDialog(); });

            ChangeUI();
        }
    }


    private void SwitchUIState(bool dialogIsActive)
    {
        UIpanel.SetActive(dialogIsActive);
        character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        character.GetComponent<Animator>().SetFloat("velocityX", 0f);
        character.GetComponent<PlayerController>().enabled = !dialogIsActive;
    }

    private void ChangeUI()
    {
        StopAllCoroutines();
        Dialog currentDialog = conversation.GetDialogByPosition(dialogPosition);

        //Starts coroutine for animated dialogs text, typewritter effect
        StartCoroutine ( AnimateText ( currentDialog.GetDialog() ));

        characterImage.sprite = returnSpriteByCharacter(currentDialog);
    }

    private Sprite returnSpriteByCharacter(Dialog currentDialog)
    {
        print(currentDialog.GetCharacter() + "-" + currentDialog.GetEmotion());
        return Resources.LoadAll<Sprite>("CharactersPictures/" + currentDialog.GetCharacter() + "-" + currentDialog.GetEmotion())[0];
        //return Array.Find(pictures, picture => picture.character == character).image;
    }

    private void DestroyDialog()
    {
        if (isDestroyable)
        {
            Destroy(gameObject);
        }
    }

    //TypeWritter effect
    IEnumerator AnimateText(string name)
    {
        isWrittingText = true;
        for (int i = 0; i < (name.Length + 1); i++)
        {
            textDialog.text = name.Substring(0, i);
            yield return new WaitForSeconds(.05f);
        }

        isWrittingText = false;
    }
}
