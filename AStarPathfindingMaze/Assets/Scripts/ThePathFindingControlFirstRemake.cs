using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ThePathFindingControlFirstRemake : MonoBehaviour
{
    public Transform startPos;
    private Grid _grid;
    public Transform endPos;
    // Start is called before the first frame update
    void Start()
    {
        _grid = GetComponent<Grid>();
        StartCoroutine(PathUpdate());
    }

    // Update is called once per frame
    

    int heuristic(PathState state, Vector2Int endPos)
    {
        var newX = Math.Abs(state.gridPosition.x - endPos.x);
        var newY = Math.Abs(state.gridPosition.y - endPos.y);
        
        
        // Debug.Log(newX+newY);
        return newX+newY;
    }

    PathState CreatStartState(Vector2Int startPos)
    {

        var pathState = new PathState();
        pathState.gridPosition = startPos;
        pathState.canJump = true;
        pathState.path.Add(_grid._grid[startPos.x, startPos.y]);
        return pathState;
    }
    void FindPath(Vector3 startPos, Vector3 endpos)
    {
        Node startNode = _grid.NodeFromWorldPoint(startPos);
        Node EndNode = _grid.NodeFromWorldPoint(endpos);
        Vector2Int endPosition = new Vector2Int(EndNode.GridX, EndNode.GridY);

        var startState = CreatStartState(new Vector2Int(startNode.GridX, startNode.GridY));
        
        List<PathState> openset = new List<PathState>();
        HashSet<PathState> closedSet = new HashSet<PathState>();
        openset.Add(startState);
        while (openset.Count>0)
        {
            Debug.Log(openset.Count);
            PathState node = openset[0];
            for (int i = 1; i < openset.Count; i++)
            {
                if (openset[i].Fcost< node.HCost)
                {
                    node = openset[i];
                }
            }

            openset.Remove(node);
            closedSet.Add(node);
            if (node.gridPosition==new Vector2Int(EndNode.GridX, EndNode.GridY))
            {
                // throw new System.Exception("RetracePath not implomented");
                RetracePath(node);
                return;
            }
            foreach (PathState neighbor in node.GetAllNeighbor(_grid))
            {
                
                //TODO In the if statement need to add int Jump so it will be able to go though the wall == does not work.
                
                int newCostToNeighbour = node.GCost + 1;
                if (newCostToNeighbour> neighbor.GCost|| !openset.Contains(neighbor))
                {
                    neighbor.GCost = newCostToNeighbour;
                    //TODO Hcost is someThing else.
                    neighbor.HCost = heuristic(neighbor, endPosition);
                    // neighbor.Parent = node;
                    if (!openset.Contains(neighbor) )
                    {
                        openset.Add(neighbor);
                    }
                }
                
                
            }
        }


    }
    void RetracePath(PathState state)
    {
        _grid.Path = new List<Node>();
        foreach (var node in state.path)
        {
            _grid.Path.Add(node);
        }
        // List<Node> path = new List<Node>();
        // Node currentNode = endNode;
        //
        // while (currentNode != startNode) {
        //     path.Add(currentNode);
        //     currentNode = currentNode.Parent;
        // }
        // path.Reverse();
        //
        // _grid.Path = path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distanceY = Mathf.Abs(nodeA.GridY - nodeB.GridY);
        if (distanceX> distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceY + 10 * (distanceY - distanceX);
    }

    private IEnumerator PathUpdate()
    {
        while (true)
        {
            _grid.ResetNodeValue();
            FindPath(startPos.position, endPos.position);
            yield return new WaitForSeconds(0.5f); 
        }
        
    }
}
