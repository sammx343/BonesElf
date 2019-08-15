using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation {
    private List<Dialog> dialogs;
    private int id;
    private bool hasOptional;
    private int optionalDialogNumber;
    
	public Conversation(int id)
    {
        this.id = id;
        dialogs = new List<Dialog>();
    }

    public Conversation(int id, bool hasOptional, int optionalDialogNumber)
    {
        this.id = id;
        this.hasOptional = hasOptional;
        this.optionalDialogNumber = optionalDialogNumber;
        dialogs = new List<Dialog>();
    }

    public void AddDialog(string dialog, Character characterId, Emotion emotion) {
        dialogs.Add(
            new Dialog(dialog, characterId, emotion)
        );
    }

    public List<Dialog> GetDialogs() {
        return dialogs;
    }

    public int GetId() {
        return id;
    }

    public Dialog GetDialogByPosition(int dialogPosition)
    {
        return dialogs[dialogPosition];
    }
    
    public bool GetHasOptional(){
        return hasOptional;
    }

    public int GetOptionalDialogNumber(){
        return optionalDialogNumber;
    }
}
