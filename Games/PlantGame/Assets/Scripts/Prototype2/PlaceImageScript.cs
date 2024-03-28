using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceImageScript : MonoBehaviour
{
    public Image image;

    private InventoryManager inventory;

    void Start()
    {
        image.enabled = false;
        inventory = InventoryManager.Instance;
        inventory.onPictureSet += OnPictureSet;
    }

    private void OnDestroy()
    {
        inventory.onPictureSet -= OnPictureSet;
    }

    public void OnPictureSet(Picture picture)
    {
        image.enabled = picture != null;
        image.sprite = picture ? picture.sprite : null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
