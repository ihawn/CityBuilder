using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Node
{
    public Vector3 Position { get; set; }
    public List<int> ConnectedNodeIds { get; set; }
    public List<float> NodeWeights { get; set; }
    public NodeProperties Properties { get; set; }
    public int Id { get; set; }

    public Node(GameObject obj)
    {
        Position = obj.transform.position;
        ConnectedNodeIds = new List<int>();
        NodeWeights = new List<float>();

        NodeProperties np = obj.GetComponent<NodeProperties>();
        Properties = np;

        ConnectedNodeIds = new List<int>();
    }
}
