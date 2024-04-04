using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sh
{
    public class Node
    {
        public Node(Vector3 Pos)
        {
            nodePos = Pos;
        }
        public Vector3 nodePos = default;
        public Color color = default;
    }

}
