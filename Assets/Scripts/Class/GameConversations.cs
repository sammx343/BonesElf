using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConversations {
    public static List<Conversation> gameConversations;

	public GameConversations()
    {
        gameConversations = new List<Conversation>();

        Conversation scene1Part1 = new Conversation(1);
        scene1Part1.AddDialog("Buenas noches dijo el elfo", Character.Elf);
        scene1Part1.AddDialog("Buenas noches dijo el dinosaurio", Character.Dinosaur);
        scene1Part1.AddDialog("Buenas noches dijo la oveja", Character.Sheep);
        scene1Part1.AddDialog("Como estamos todos aquí? Pasandola bueno", Character.Elf);
        scene1Part1.AddDialog("Sisa mani, bien clarinete", Character.Dinosaur);
        scene1Part1.AddDialog("Pinoleta suave socio", Character.Sheep);
        scene1Part1.AddDialog("Nos pillamos compita", Character.Elf);
        scene1Part1.AddDialog("Sisa, suerte es que te digo", Character.Sheep);
        addDialogs(scene1Part1);

        Conversation scene1Part2 = new Conversation(2);
        scene1Part2.AddDialog("Que pasá o que wey wefwe fwefwef wef ef e fwef we fwef wefwe fwef wefwef wef wefwefwef wefw efwef wefwe fwefwefwefew wefwef", Character.Sheep);
        scene1Part2.AddDialog("Que pasá o que wey", Character.Dinosaur);
        scene1Part2.AddDialog("No sé, me trajeron aqui sin previo aviso", Character.Witch);
        scene1Part2.AddDialog("Más tarde les tiro el secreto man", Character.Elf);
        addDialogs(scene1Part2);

        Conversation scene1Part3 = new Conversation(3);
        scene1Part3.AddDialog("Que pasá wefwe o que wey", Character.Sheep);
        scene1Part3.AddDialog("Que pasá wefwef o que wey", Character.Dinosaur);
        scene1Part3.AddDialog("No sé, wefewf me trajeron aqui sin previo aviso", Character.Witch);
        scene1Part3.AddDialog("Más tarde wefwef les tiro el secreto man", Character.Elf);
        addDialogs(scene1Part3);
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
