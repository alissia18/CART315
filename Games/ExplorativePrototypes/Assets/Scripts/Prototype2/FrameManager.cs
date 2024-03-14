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
    public Frame ReturnFrame()
    {
        if (frames.Count > 1)
        {
            CurrentFrame.UnsetFrame();
            Frame f = frames.Pop();
            CurrentFrame.SetFrame();
            return f;
        }
        
        return null;
    }
}