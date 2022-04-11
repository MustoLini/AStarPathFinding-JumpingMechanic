using UnityEngine;

public class Node
{
    public bool Walkable;
    public Vector3 WorldPosition;
    public readonly int GridX;
    public readonly int GridY;
    
    public readonly GameObject cube;
    // public int GCost;
    // public int HCost;
   
    public int PathLeaghtBeforeJump=int.MaxValue;
    public int PathLeaghtAfterJump=int.MaxValue;
    public readonly MeshRenderer mesh;
    

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY, GameObject cube)
    {
        this.cube = cube;
        mesh = this.cube.GetComponent<MeshRenderer>();
        this.Walkable = walkable;
        this.WorldPosition = worldPosition;
        this.GridX = gridX;
        this.GridY = gridY;
    }

    
    // public int Fcost => GCost + HCost;
}