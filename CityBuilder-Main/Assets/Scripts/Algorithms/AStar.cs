using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using C5;

public static class AStar
{
    public static List<Node> GetShortestPath(Node start, Node end, List<Node> allNodes)
    {
        List<Node> path = new List<Node>();
        var openHeap = new IntervalHeap<Node>(new NodeComparer());
        start.Open = true;
        openHeap.Add(start);

        Node current;

        while(!openHeap.IsEmpty)
        {
            current = openHeap.DeleteMin();
            allNodes[current.Id].Open = false;
            allNodes[current.Id].Closed = true;

            if(current.Id == end.Id)
            {
                while(current.Id != start.Id)
                {
                    path.Add(current);
                    current = allNodes[current.ParentId];
                }

                break;
            }

            foreach(int i in current.ConnectedNodeIds)
            {
                if(allNodes[i].Open || allNodes[i].Closed) { continue; }

                allNodes[i].G = allNodes[current.Id].G + allNodes[current.Id].EdgeWeights[allNodes[current.Id].ConnectedNodeIds.IndexOf(i)];
                allNodes[i].F = allNodes[i].G + Vector3.Distance(allNodes[i].Position, end.Position);
                allNodes[i].Open = true;
                allNodes[i].Closed = false;
                allNodes[i].ParentId = current.Id;
                openHeap.Add(allNodes[i]);
            }
        }

        return path;
    }
}
