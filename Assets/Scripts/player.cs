using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    public TextMeshProUGUI descriptionText;
    public GameObject trap;

    NPC currentNPC;
    Queue<NPC> NPCQueue;
    GameObject currentTrap;

    void Start()
    {
        InitiateNPCQueue();
        // Differentiate heaven and hell
        heavenButton.onClick.AddListener(SendToHeaven);
        hellButton.onClick.AddListener(SendToHell);
        if (NPCQueue.Count > 0)
        {
            UpdateNPC();
        }
    }

    void InitiateNPCQueue()
    {
        currentTrap = Instantiate(trap);
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

    void SendToHeaven()
    {
        EditorUtility.DisplayDialog("Decisión!", "Enviaste a " + currentNPC.name + " al cielo", ":D", ":(");
        MoveToNextNPC();
    }

    void SendToHell()
    {
        StartCoroutine(Waiter());
    }

    void MoveToNextNPC()
    {
        if (currentTrap == null)
        {
            currentTrap = Instantiate(trap);
        }
        if (NPCQueue.Count == 0)
        {
            EditorUtility.DisplayDialog("Fin", "Gracias por jugar la demo!", "ok", ":(");
            return;
        }
        Destroy(currentNPC.gameObject);
        UpdateNPC();
    }

    void UpdateNPC()
    {
        currentNPC = NPCQueue.Dequeue();
        currentNPC.gameObject = Instantiate(currentNPC.gameObject);
        descriptionText.text = currentNPC.description;
        //Debug.Log(currentNPC.name);
        //Debug.Log(currentNPC.description);
    }

    void ActivateTrap()
    {
        trap.transform.Rotate(new Vector3(90f, 0));
    }

    IEnumerator Waiter()
    {
        Destroy(currentTrap);
        currentTrap = null;
        yield return new WaitForSeconds(2);
        EditorUtility.DisplayDialog("Decisión!", "Enviaste a " + currentNPC.name + " al infierno", ":D", ":(");
        MoveToNextNPC();
    }
}
