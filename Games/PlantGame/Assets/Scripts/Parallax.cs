using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Camera cam;
    public List<Layer> layers;
    
    private void Start()
    {
        cam = Camera.main;

        foreach (Layer layer in layers)
            layer.Init();
    }

    private void LateUpdate()
    {
        foreach (Layer layer in layers)
        {
            Vector2 dist = (Vector2)cam.transform.position - layer.startPos;

            dist.x *= layer.parallaxFactor.x;
            dist.y *= layer.parallaxFactor.y;

            Vector2 newPos = layer.startPos + dist;

            if (layer.anchor != null) layer.anchor.position = newPos;
        }
    }

    [System.Serializable]
    public class Layer
    {
        public Transform anchor;
        public Vector2 parallaxFactor;

        [HideInInspector] public Vector2 startPos;

        public void Init()
        {
            if (anchor == null) return;

            startPos = anchor.position;
        }
    }
}
