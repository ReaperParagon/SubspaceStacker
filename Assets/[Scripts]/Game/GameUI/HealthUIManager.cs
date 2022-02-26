using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> livesUI = new List<GameObject>();
    public void UpdateUI(int lives)
    {
        if (livesUI == null)
            return;

        for (int index = 0; index < livesUI.Count; index++)
        {
            if (livesUI[index] == null)
                continue;

            livesUI[index].SetActive(index < lives);
        }
    }
}
