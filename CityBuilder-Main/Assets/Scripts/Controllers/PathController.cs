using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathController
{
    public static List<Node> GetRandomPath()
    {
        //temporary until a* is implemented
        List<Node> nodes = GlobalSettings.GameManager.Nodes;
        List<Node> path = new List<Node>();
        Node start = nodes[0];
        Node end = nodes[nodes.Count - 1];

        Node currentNode = start;
        int k = 0;
        while(currentNode.Id != end.Id && k < 100)
        {
            path.Add(currentNode);

            int choice;
            if (currentNode.ConnectedNodeIds.Count > 2)
                choice = Random.Range(1, currentNode.ConnectedNodeIds.Count);
            else if (currentNode.ConnectedNodeIds.Count == 1 && k > 0)
                break;
            else if (currentNode.ConnectedNodeIds.Count == 1 && k == 0)
                choice = 0;
            else
                choice = 1;
            currentNode = nodes[currentNode.ConnectedNodeIds[choice]];

            k++;
        }

        return path;
    }
}
