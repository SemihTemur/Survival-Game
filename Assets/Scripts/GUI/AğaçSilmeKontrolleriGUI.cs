using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AğaçSilmeKontrolleriGUI : MonoBehaviour
{
    public static AğaçSilmeKontrolleriGUI Instance { get; private set; }

    public Slider slider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }
    }
}
