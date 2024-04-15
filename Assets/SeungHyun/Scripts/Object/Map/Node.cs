using System;
using UnityEngine;

[Flags]
public enum NodeColor : int
{
    Red, Yellow, Magenta, Cyan, Green, Blue, Gray, Black, None
}

public class Node
{
    public Node(Vector3 Pos)
    {
        nodePos = Pos;
    }
    public Vector3 nodePos = default;
    public Color color = default;
    public NodeColor nodeColor = NodeColor.None;
    public void SetColor(Color newColor)
    {
        color = newColor;
        if (newColor == Color.red)
        {
            nodeColor = NodeColor.Red;
        }
        else if (newColor == Color.yellow)
        {
            nodeColor = NodeColor.Yellow;
        }
        else if (newColor == Color.magenta)
        {
            nodeColor = NodeColor.Magenta;
        }
        else if (newColor == Color.cyan)
        {
            nodeColor = NodeColor.Cyan;
        }
        else if (newColor == Color.green)
        {
            nodeColor = NodeColor.Green;
        }
        else if (newColor == Color.blue)
        {
            nodeColor = NodeColor.Blue;
        }
        else if (newColor == Color.gray)
        {
            nodeColor = NodeColor.Gray;
        }
        else if (newColor == Color.black)
        {
            nodeColor = NodeColor.Black;
        }
    }

    public override string ToString()
    {
        return nodePos.ToString();
    }
}


