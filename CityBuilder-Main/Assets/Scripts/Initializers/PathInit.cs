using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathInit : MonoBehaviour
{
    public List<GameObject> RoadGameObjects;
    public List<NodeProperties> NodeGameObjects;

    public void Init()
    {
        foreach(GameObject g in RoadGameObjects)
        {
            //Join node parents by their socket nodes
            List<NodeProperties> nodes1 = new List<NodeProperties>();
            foreach(Transform t in g.transform)
                if(t.name.ToLower().Contains("node"))
                    nodes1.Add(t.GetComponent<NodeProperties>());

            List<NodeProperties> socketNodes1 = nodes1
                .Where(x => x.gameObject.tag == "NodeSocket")
                .ToList();

            RoadProperties rp1 = g.GetComponent<RoadProperties>();
            foreach(RoadProperties rp2 in rp1.ConnectedRoadObjects)
            {
                List<NodeProperties> nodes2 = new List<NodeProperties>();
                foreach (Transform t in rp2.transform)
                    if (t.name.ToLower().Contains("node"))
                        nodes2.Add(t.GetComponent<NodeProperties>());

                List<NodeProperties> socketNodes2 = nodes2
                    .Where(x => x.gameObject.tag == "NodeSocket")
                    .ToList();

                NodeProperties[] toJoin = new NodeProperties[] { socketNodes1[0], socketNodes2[0] };
                float minDist = float.MaxValue;
                for(int i = 0; i < socketNodes1.Count; i++)
                {
                    for(int j = 0; j < socketNodes2.Count; j++)
                    {
                        float dist = Vector3.Distance(socketNodes1[i].transform.position, socketNodes2[j].transform.position);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            toJoin = new NodeProperties[] { socketNodes1[i], socketNodes2[j] };
                        }
                    }
                }

                toJoin[0].ConnectedNodeGameObjects.Add(toJoin[1].gameObject);            
            }

            NodeGameObjects.AddRange(nodes1);
            //End join
        }
        int id = 0;
        foreach(NodeProperties np in NodeGameObjects)
        {
            np.Id = id;
            id++;
        }

        foreach(NodeProperties np in NodeGameObjects)
        {
            Node n = new Node(np.gameObject);
            n.Id = np.Id;
            n.ConnectedNodeIds = np.ConnectedNodeGameObjects
                .Select(x => x.GetComponent<NodeProperties>().Id)
                .ToList();
            GlobalSettings.GameManager.Nodes.Add(n);
            np.ThisNode = n;
        }
    }
}
