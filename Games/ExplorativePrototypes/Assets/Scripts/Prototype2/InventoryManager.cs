using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Picture heldPicture;

    public static InventoryManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void AddToInventory(Picture p)
    {
        if (p)
        {
            heldPicture = p;
        }
        
    }

    public Picture RemoveFromInventory()
    {
        Picture oldPicture = heldPicture;
        heldPicture = null;
        return oldPicture;
    }
}
