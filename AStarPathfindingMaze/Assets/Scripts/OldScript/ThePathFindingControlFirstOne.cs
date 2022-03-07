// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class ThePathFindingControlFirstOne : MonoBehaviour
// {
//     
//     public Transform startPos;
//     private Grid _grid;
//     public Transform endPos;
//     // Start is called before the first frame update
//     void Start()
//     {
//         _grid = GetComponent<Grid>();
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         FindPath(startPos.position, endPos.position);
//     }
//
//     void FindPath(Vector3 startPos, Vector3 endpos)
//     {
//         // TODO Find the Ideal place to jump to get to the end position at the shortest time possible. 
//         Node startNode = _grid.NodeFromWorldPoint(startPos);
//         Node EndNode = _grid.NodeFromWorldPoint(endpos);
//
//         List<Node> openset = new List<Node>();
//         HashSet<Node> closedSet = new HashSet<Node>();
//         openset.Add(startNode);
//         while (openset.Count>0)
//         {
//             Node node = openset[0];
//             for (int i = 1; i < openset.Count; i++)
//             {
//                 if (openset[i].Fcost< node.HCost)
//                 {
//                     node = openset[i];
//                 }
//             }
//
//             openset.Remove(node);
//             closedSet.Add(node);
//             if (node == EndNode) {
//                 RetracePath(startNode,EndNode);
//                 return;
//             }
//             foreach (Node neighbor in _grid.GetNeighbours(node))
//             {
//                 //TODO In the if statement need to add int Jump so it will be able to go though the wall == does not work.
//                 if (!neighbor.Walkable || closedSet.Contains(neighbor))
//                 {
//                     continue;
//                 }
//
//                 int newCostToNeighbour = node.GCost + GetDistance(node, neighbor)+ neighbor.movementPenalty;
//                 if (newCostToNeighbour> neighbor.GCost|| !openset.Contains(neighbor))
//                 {
//                     neighbor.GCost = newCostToNeighbour;
//                     neighbor.HCost = GetDistance(neighbor, EndNode);
//                     neighbor.Parent = node;
//                     if (!openset.Contains(neighbor))
//                     {
//                         openset.Add(neighbor);
//                     }
//                 }
//             }
//         }
//        
//         // TODO Check the BritDog to find solutions where you can add the findpath.
//     }
//     void RetracePath(Node startNode, Node endNode) {
//         List<Node> path = new List<Node>();
//         Node currentNode = endNode;
//
//         while (currentNode != startNode) {
//             path.Add(currentNode);
//             currentNode = currentNode.Parent;
//         }
//         path.Reverse();
//
//         _grid.Path = path;
//
//     }
//
//     int GetDistance(Node nodeA, Node nodeB)
//     {
//         int distanceX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
//         int distanceY = Mathf.Abs(nodeA.GridY - nodeB.GridY);
//         if (distanceX> distanceY)
//         {
//             return 14 * distanceY + 10 * (distanceX - distanceY);
//         }
//         return 14 * distanceY + 10 * (distanceY - distanceX);
//     }
//     
// }
