using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Picture : MonoBehaviour

{
    private Canvas canvas;
    public FrameManager manager;
    public InventoryManager inventory;
    public Frame connectedFrame;
    public Sprite sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        if (canvas = GetComponentInChildren<Canvas>())
        {
            canvas.worldCamera = Camera.main;
        }

        manager = FrameManager.Instance;
        inventory = InventoryManager.Instance;
    }

    [Button]
    public void OnClick()
    {
        manager.SetFrame(connectedFrame);
    }

    [Button]
    public void OnPickup()
    {
        inventory.AddToInventory(this);
        this.gameObject.SetActive(false);
    }

    public void Pickup()
    {
        manager.PickupPicture();
    }

    public void Place()
    {
        this.gameObject.SetActive(true);
    }
}