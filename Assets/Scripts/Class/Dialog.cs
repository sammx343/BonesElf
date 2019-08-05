using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog {
    private string dialog;
    private Character character;
    private Emotion emotion;

	public Dialog(string dialog, Character character, Emotion emotion)
    {
        this.dialog = dialog;
        this.character = character;
        this.emotion = emotion;
    }

    public string GetDialog()
    {
        return dialog;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public Emotion GetEmotion()
    {
        return emotion;
    }
}
