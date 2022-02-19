using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathInit : MonoBehaviour
{
    public List<GameObject> RoadGameObjects;
    public List<GameObject> NodeGameObjects;

    public void Init()
    {
        foreach(GameObject g in RoadGameObjects)
        {
            List<NodeProperties> nodes1 = new List<NodeProperties>();
            foreach(Transform t in g.transform)
                if(g.name.ToLower().Contains("node"))
                    nodes1.Add(t.GetComponent<NodeProperties>());

            List<NodeProperties> nullRp1 = nodes1
                .Where(x => x.ConnectedNodeGameObjects.Count == 1)
                .ToList();

            RoadProperties rp1 = g.GetComponent<RoadProperties>();
            foreach(RoadProperties rp2 in rp1.ConnectedRoadObjects)
            {
                List<NodeProperties> nodes2 = new List<NodeProperties>();
                foreach (Transform t in g.transform)
                    if (g.name.ToLower().Contains("node"))
                        nodes1.Add(t.GetComponent<NodeProperties>());

                List<NodeProperties> nullRp2 = nodes2
                    .Where(x => x.ConnectedNodeGameObjects.Count == 1)
                    .ToList();

                NodeProperties[] toJoin = new NodeProperties[] { nodes1[0], nodes2[0] };
                float minDist = float.MaxValue;
                for(int i = 0; i < nodes1.Count; i++)
                {
                    for(int j = 0; j < nodes2.Count; j++)
                    {
                        float dist = Vector3.Distance(nodes1[i].transform.position, nodes2[j].transform.position);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            toJoin = new NodeProperties[] { nodes1[i], nodes2[j] };
                        }
                    }
                }

                toJoin[0].ConnectedNodeGameObjects.Add(toJoin[1].gameObject);
                toJoin[1].ConnectedNodeGameObjects.Add(toJoin[0].gameObject);
            }
        }

        int id = 0;
        foreach(GameObject g in NodeGameObjects)
        {
            if (g.name.ToLower().Contains("node"))
            {
                NodeProperties np = g.GetComponent<NodeProperties>();
                Node n = new Node(g);
                np.Id = id;
                n.Id = id;
                GlobalSettings.GameManager.Nodes.Add(n);
                id++;
            }
        }
    }
}
