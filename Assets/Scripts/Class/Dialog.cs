using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog {
    private string dialog;
    private Character character;

	public Dialog(string dialog, Character character)
    {
        this.dialog = dialog;
        this.character = character;
    }

    public string GetDialog()
    {
        return dialog;
    }

    public Character GetCharacter()
    {
        return character;
    }
}
