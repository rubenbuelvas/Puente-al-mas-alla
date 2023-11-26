using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC
{
    public GameObject gameObject;
    public string name;
    public string description;
}

public class player : MonoBehaviour
{
    public List<GameObject> NPCGameObjectList;
    public List<string> NPCNameList;
    public List<string> NPCDescriptionList;
    public Button heavenButton;
    public Button hellButton;

    NPC currentNPC;
    Queue<NPC> NPCQueue;

    void Start()
    {
        InitiateNPCQueue();
        heavenButton.onClick.AddListener(MoveToNextNPC);
        hellButton.onClick.AddListener(MoveToNextNPC);
        if (NPCQueue.Count > 0)
        {
            UpdateNPC();
        }
    }

    void InitiateNPCQueue()
    {
        NPCQueue = new Queue<NPC>();
        for (int i = 0; i < NPCGameObjectList.Count; i++)
        {
            NPC npc = new NPC();
            npc.gameObject = NPCGameObjectList[i];
            npc.name = NPCNameList[i];
            npc.description = NPCDescriptionList[i];
            NPCQueue.Enqueue(npc);
        }
    }

    void MoveToNextNPC()
    {
        if (NPCQueue.Count == 0)
        {
            return;
        }
        Destroy(currentNPC.gameObject);
        UpdateNPC();
    }

    void UpdateNPC()
    {
        currentNPC = NPCQueue.Dequeue();
        currentNPC.gameObject = Instantiate(currentNPC.gameObject);
        Debug.Log(currentNPC.name);
        Debug.Log(currentNPC.description);
    }
}
