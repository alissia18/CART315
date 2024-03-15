using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public Frame firstFrame;
    public static FrameManager Instance;
    private Stack<Frame> frames;

    public Frame CurrentFrame
    {
        get => frames.Peek();
    }

    void Awake()
    {
        Instance = this;
        frames = new Stack<Frame>();
    }

    void Start()
    {
        SetFrame(firstFrame);
    }
    public void SetFrame(Frame frame)
    {
        if (!frame) return;

        if (frames.Count > 0)
        {
            CurrentFrame.UnsetFrame();
        }

        frames.Push(frame);
        frame.SetFrame();
    }

    [Button]
    public void ReturnFrame()
    {
        if (frames.Count > 1)
        {
            CurrentFrame.UnsetFrame();
            frames.Pop();
            CurrentFrame.SetFrame();

        }
        
    }

    public void PickupPicture()
    {
        if (CurrentFrame != null)
        {
            Debug.Log(CurrentFrame.name);
            CurrentFrame.PickupPicture();
        }
    }

    public void PlacePicture()
    {
        if (CurrentFrame != null)
        {
            CurrentFrame.PlacePicture();
        }
    }
}