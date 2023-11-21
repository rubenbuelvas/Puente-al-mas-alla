using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public List<GameObject> NPCList;
    public Button heavenButton;
    public Button hellButton;

    GameObject currentNPC;
    Queue<GameObject> NPCQueue;


    // Start is called before the first frame update
    void Start()
    {
        NPCQueue = new Queue<GameObject>(NPCList);
        heavenButton.onClick.AddListener(MoveToNextNPC);
        hellButton.onClick.AddListener(MoveToNextNPC);
        if (NPCQueue.Count > 0)
        {
            currentNPC = Instantiate(NPCQueue.Dequeue());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveToNextNPC()
    {
        if (NPCList.Count == 0)
        {
            return;
        }
        Destroy(currentNPC);
        currentNPC = Instantiate(NPCQueue.Dequeue());
    }
}
