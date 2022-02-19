using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Node
{
    public Vector3 Position { get; set; }
    public List<int> ConnectedNodeIds { get; set; }
    public NodeProperties Properties { get; set; }
    public int Id { get; set; }
    public int ParentId { get; set; }
    public List<float> EdgeWeights { get; set; }
    public float F { get; set; }
    public float G { get; set; }
    public float H { get; set; }
    public bool Open { get; set; }
    public bool Closed { get; set; }

    public Node(GameObject obj)
    {
        Position = obj.transform.position;
        ConnectedNodeIds = new List<int>();
        EdgeWeights = new List<float>();

        NodeProperties np = obj.GetComponent<NodeProperties>();
        Properties = np;

        ConnectedNodeIds = np.ConnectedNodeGameObjects
                .Select(x => x.GetComponent<NodeProperties>().Id)
                .ToList();

        Open = false;
        Closed = false;
        F = 0;
        G = 0;
        H = 0;
    }
}

public class NodeComparer : IComparer<Node>
{
    public int Compare(Node x, Node y)
    {
        return x.F.CompareTo(y.F);
    }
}
