using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modal : MonoBehaviour
{
    public Button modalButton;

    void Start()
    {
        modalButton.onClick.AddListener(Hide);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
