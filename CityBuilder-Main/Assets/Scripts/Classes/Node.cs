using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Vector3 Position { get; set; }
    public List<Node> ConnectedNodes { get; set; }
    public List<float> NodeWeights { get; set; }
    public NodeProperties Properties { get; set; }
    public int Id { get; set; }

    public Node(GameObject obj)
    {
        Position = obj.transform.position;
        ConnectedNodes = new List<Node>();
        NodeWeights = new List<float>();

        NodeProperties np = obj.GetComponent<NodeProperties>();
        Properties = np;

        ConnectedNodes = np.ConnectedNodeGameObjects.Select(x => x.GetComponent<NodeProperties>().ThisNode).ToList();
    }
}
