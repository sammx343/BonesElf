using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation {
    private List<Dialog> dialogs;
    private int id;

	public Conversation(int id)
    {
        this.id = id;
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
}
