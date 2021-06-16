using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferManager : MonoBehaviour
{
    public static TransferManager instance = null;

    public List<int> selected_packs = new List<int>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
     
    public void ClearSelectedPacks()
    {
        selected_packs.Clear();
    }
}