using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConversations {
    public static List<Conversation> gameConversations;

	public GameConversations()
    {
        gameConversations = new List<Conversation>();
        
        Conversation scene1Part1 = new Conversation(1);
        scene1Part1.AddDialog("Aja viejo Gnomo, como va la vaina", Character.Elf, Emotion.feliz);
        scene1Part1.AddDialog("Ahí viejo Elfo, llevandola barro", Character.Gnom, Emotion.triste);
        scene1Part1.AddDialog("Eche y por qué viejo man?", Character.Elf, Emotion.normal);
        scene1Part1.AddDialog("Joda viejo Elfo, la hada cachona esa vale, pilla lo que está haciendo, me está robando marica", Character.Gnom, Emotion.rabioso);
        scene1Part1.AddDialog("Eche cómo así .... a lo bien?", Character.Elf, Emotion.hmm);
        scene1Part1.AddDialog("Sisa mani, a lo bien, pa que veas tú", Character.Gnom, Emotion.triste);
        addDialogs(scene1Part1);

        Conversation scene1Part2 = new Conversation(2);
        scene1Part2.AddDialog("Pues si man, como la víste?", Character.Gnom, Emotion.triste);
        scene1Part2.AddDialog("Barro, esa hada está pesada", Character.Elf, Emotion.rabioso);
        scene1Part2.AddDialog("Ven acá man, y no será que tú ... Joa ?", Character.Gnom, Emotion.feliz);
        scene1Part2.AddDialog("Tirala", Character.Elf, Emotion.normal);
        scene1Part2.AddDialog("Joa no será que tú me puedes hacer el dos capturando al hada pecueca esa?", Character.Gnom, Emotion.feliz);
        scene1Part2.AddDialog("Sábes qué? Eso va", Character.Elf, Emotion.aburrido);
        addDialogs(scene1Part2);
    }

    public Conversation GetConversationById(int conversationId) {
        Conversation conversation = new Conversation(0);
        for (int i = 0; i < gameConversations.Count ; i++)
        {
            if (gameConversations[i].GetId() == conversationId) {
                conversation = gameConversations[i];
            }
            
        }
        return conversation;
    }

    private void addDialogs(Conversation conversation)
    {
        gameConversations.Add(conversation);
    }
}
