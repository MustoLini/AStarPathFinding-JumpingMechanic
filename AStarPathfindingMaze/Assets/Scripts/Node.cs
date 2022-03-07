using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool Walkable;
    public Vector3 WorldPosition;
    public readonly int GridX;
    public readonly int GridY;
    public int movementPenalty;
    
    // public int GCost;
    // public int HCost;
    public Node Parent;
    public int PathLeaghtBeforeJump=int.MaxValue;
    public int PathLeaghtAfterJump=int.MaxValue;
    public Node PreviousNode;
    public bool CanJump;

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY, int _movementPenalty)
    {
        this.Walkable = walkable;
        this.WorldPosition = worldPosition;
        this.GridX = gridX;
        this.GridY = gridY;
        this.movementPenalty = _movementPenalty;
    }

    
    // public int Fcost => GCost + HCost;
}