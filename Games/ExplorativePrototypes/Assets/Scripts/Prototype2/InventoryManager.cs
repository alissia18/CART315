using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public event Action<Picture> onPictureSet;
    
    public Picture heldPicture;

    public static InventoryManager Instance;

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
            onPictureSet?.Invoke(p);
        }
        
    }

    public Picture RemoveFromInventory()
    {
        Picture oldPicture = heldPicture;
        heldPicture = null;
        onPictureSet?.Invoke(null);
        return oldPicture;
    }
}
