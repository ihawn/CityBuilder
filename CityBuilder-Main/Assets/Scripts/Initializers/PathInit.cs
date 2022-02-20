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

        //Initialize Ids
        int id = 0;
        foreach(NodeProperties np in NodeGameObjects)
        {
            np.Id = id;
            id++;
        }

        //Initialize nodes
        foreach(NodeProperties np in NodeGameObjects)
        {
            Node n = new Node(np.gameObject);
            n.Id = np.Id;
            GlobalSettings.GameManager.Nodes.Add(n);
            np.ThisNode = n;
        }

        GameObject go = new GameObject("Node Transforms");
        //Initialize node weights
        GameManager gm = GlobalSettings.GameManager;
        foreach (Node node in GlobalSettings.GameManager.Nodes)
        {
            GlobalSettings.GameManager.Nodes[node.Id].EdgeWeights
                = gm.Nodes[node.Id].ConnectedNodeIds
                .Select(x => Vector3.Distance(gm.Nodes[x].Position, gm.Nodes[node.Id].Position))
                .ToList();
            
            GameObject go2 = new GameObject("node" + node.Id);
            go2.transform.position = node.Position;
            go2.AddComponent<SphereCollider>();
            go2.GetComponent<SphereCollider>().radius = 0.1f;
            go2.transform.parent = go.transform;

        }
    }
}
