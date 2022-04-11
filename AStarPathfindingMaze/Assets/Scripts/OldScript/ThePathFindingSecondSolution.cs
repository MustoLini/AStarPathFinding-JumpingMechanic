using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class ThePathFindingSecondSolution : MonoBehaviour
// {
//    public Transform startPos;
//    private Grid _grid;
//    public Transform endPos;
//    public bool foundNextOne;
//    public int jumpMechanic = 2;
//    
//     // Start is called before the first frame update
//     void Start()
//     {
//        _grid = GetComponent<Grid>();
//        
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//        RetracePath(FindPath(startPos.position, endPos.position));
//     }
//     
//     Stack<Node> FindPath(Vector3 startPos, Vector3 endPos, int depth)
//     {
//       
//        List<Node> visitedNodes = new List<Node>();
//        Stack<Node> path = new Stack<Node>();
//        Node startPath = _grid.NodeFromWorldPoint(startPos);
//        Node endNode = _grid.NodeFromWorldPoint(endPos);
//        path.Push(startPath);
//        visitedNodes.Add(startPath);
//        
//        while (path.Count!=0)
//        {
//           foundNextOne = false;
//           foreach (Node neighbor in _grid.GetNeighbours(path.Peek()))
//           {
//              if(!neighbor.Walkable|| visitedNodes.Contains(neighbor))
//              {
//                 continue;
//              }
//              else if (depth>0)
//              {
//                 visitedNodes.Add(neighbor);
//                 path.Push(neighbor);
//                 depth--;
//                 if (neighbor == endNode)
//                 {
//                    return path;
//                 }
//                 foundNextOne = true;
//                 break;
//              }
//           }
//           if (!foundNextOne)
//           {
//              path.Pop();
//              depth++;
//           }
//        }
//        return null;
//     }
//     
//     /*
//      procedure find_path(start_node, end_node)
//    depth = 1
//    while(true)
//       result = find_path(start_node, end_node, depth++)
//       if(result != null)
//          return result
//       end if
//    end while
// end procedure
//      */
//
//     Stack<Node> FindPath(Vector3 startPos, Vector3 endPos)
//     {
//        int depth = 1;
//        while (depth<=900)
//        {
//           Stack<Node> result = FindPath(startPos, endPos, depth++);
//           if (result!= null)
//           {
//              return result;
//           }
//        }
//        throw new Exception("Found No path");
//     }
//     void RetracePath(Stack<Node>nodes)
//     {
//        _grid.Path = new();
//        _grid.Path.AddRange(nodes);
//        // foreach (var node in nodes)
//        // {
//        //    Gizmos.color= Color.blue;
//        //    
//        // }
//        // List<Node> path = new List<Node>();
//        // Node currentNode = endNode;
//        //
//        // while (currentNode != startNode) {
//        //    path.Add(currentNode);
//        //    currentNode = currentNode.Parent;
//        // }
//        // path.Reverse();
//        //
//        // _grid.Path = path;
//
//     }
//     
// }
//
