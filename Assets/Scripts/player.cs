using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC
{
    public GameObject gameObject;
    public string name;
    public string description;
    public bool goesToHeaven;
}

public class player : MonoBehaviour
{
    public List<GameObject> NPCGameObjectList;
    public List<string> NPCNameList;
    public List<string> NPCDescriptionList;
    public List<bool> NPCGoesToHeavenList;

    public Button heavenButton;
    public Button hellButton;
    public TextMeshProUGUI descriptionText;
    public GameObject modal;
    public TextMeshProUGUI modalHeader;
    public TextMeshProUGUI modalBody;
    public Button modalButton;
    public TextMeshProUGUI modalButtonText;
    public GameObject trap;

    NPC currentNPC;
    Queue<NPC> NPCQueue;
    GameObject currentTrap;
    int points;
    int minimumPoints;

    void Start()
    {
        modal.SetActive(false);
        InitiateNPCQueue();
        heavenButton.onClick.AddListener(SendToHeaven);
        hellButton.onClick.AddListener(SendToHell);
        modalButton.onClick.AddListener(MoveToNextNPC);
        if (NPCQueue.Count > 0)
        {
            UpdateNPC();
        }
        points = 0;
        minimumPoints = 2;
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
            npc.goesToHeaven = NPCGoesToHeavenList[i];
            NPCQueue.Enqueue(npc);
        }
    }

    void SendToHeaven()
    {
        ActivateModal("¡Decisión!", "Enviaste a " + currentNPC.name + " al cielo", "Siguiente");
        if (currentNPC.goesToHeaven)
        {
            points++;
        }
    }

    void SendToHell()
    {
        if (!currentNPC.goesToHeaven)
        {
            points++;
        }
        StartCoroutine(Waiter());
    }

    void MoveToNextNPC()
    {
        Debug.Log(points);
        Debug.Log(minimumPoints);
        if (currentTrap == null)
        {
            currentTrap = Instantiate(trap);
        }
        if (NPCQueue.Count == 0)
        {
            modalButton.onClick.AddListener(RestartScene);
            if (points < minimumPoints)
            {
                ActivateModal("Perdiste :(", "No tomaste buenas decisiones. Inténtalo de nuevo.", "Reiniciar");
            }
            else
            {
                ActivateModal("Ganaste :D", "Tomaste buenas decisiones. ¡Gracias por jugar!", "Reiniciar");
            }
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

    IEnumerator Waiter()
    {
        Destroy(currentTrap);
        currentTrap = null;
        yield return new WaitForSeconds(2);
        ActivateModal("¡Decisión!", "Enviaste a " + currentNPC.name + " al infierno", "Siguiente");
    }

    void ActivateModal(string header, string body, string buttonText)
    {
        modal.SetActive(true);
        modalHeader.text = header;
        modalBody.text = body;
        modalButtonText.text = buttonText;
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
