using System.Collections.Generic;
using UnityEngine;

public class PathState 
{
    public Vector2Int gridPosition;
    public List<Node> path= new List<Node>();
    public bool canJump;
    public readonly Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left}; 
    public int GCost;
    public int HCost;
    public int Fcost => GCost + HCost;
    
    public IEnumerable<PathState> GetAllNeighbor(Grid grid)
    {
        foreach (var direction in directions)
        {
            var searthPostion = gridPosition + direction;
            if (grid.OutOfBound(searthPostion))
            {
                continue;
            }

            var searthNode = grid._grid[searthPostion.x, searthPostion.y];
            if (path.Contains(searthNode))
            {
                continue;
            }
            
            if (!searthNode.Walkable)
            {
                if (canJump)
                {
                    var jumpTargetPostion = searthPostion + direction;
                    if (grid.OutOfBound(jumpTargetPostion))
                    {
                        continue;
                    }
                        
                    
                    var doubleSearthNode = grid._grid[jumpTargetPostion.x, jumpTargetPostion.y];
                    if (!doubleSearthNode.Walkable)
                    {
                        continue;
                    }
                    if (path.Contains(doubleSearthNode))
                    {
                        continue;
                    }

                    if (doubleSearthNode.PathLeaghtAfterJump <= GCost )
                    {
                        Debug.Log("Has Jumped");
                        continue;
                    }
                    Debug.Log("Jump State");
                    doubleSearthNode.PathLeaghtAfterJump = GCost;
                    PathState AllowedState = new PathState();
                    AllowedState.gridPosition = jumpTargetPostion;
                    AllowedState.path = new List<Node>(path);
                    AllowedState.path.Add(doubleSearthNode);
                    AllowedState.canJump = false;
                    yield return AllowedState;
                }
                continue;
            }

            if (canJump)
            {
                if (searthNode.PathLeaghtBeforeJump <= GCost)
                {
                    continue;
                } 
                searthNode.PathLeaghtBeforeJump = GCost;
            }
            else
            {
                if (searthNode.PathLeaghtAfterJump <= GCost)
                {
                    continue;
                } 
                searthNode.PathLeaghtAfterJump = GCost;
            }
            

            
            PathState NewState = new PathState();
            NewState.gridPosition = searthPostion;
            NewState.path = new List<Node>(path);
            NewState.path.Add(searthNode);
            NewState.canJump = canJump;
            yield return NewState;
            
        }
    }
}
