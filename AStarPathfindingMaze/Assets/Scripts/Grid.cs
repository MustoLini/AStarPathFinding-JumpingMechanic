using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;
    public LayerMask unWalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Node[,] _grid;
    public TerrainType[] walkableRegions;
    private LayerMask walkableMask;
    private float _nodeDiameter;
    private int _gridSizeX, _gridSizeY;
    private Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();
   
    


    // Start is called before the first frame update
    void Awake()
    {
        _nodeDiameter = nodeRadius * 2;
        _gridSizeX = (int) (gridWorldSize.x/ _nodeDiameter);
        _gridSizeY = (int) (gridWorldSize.y / _nodeDiameter);
        foreach (var region in walkableRegions)
        {
            walkableMask.value  |= region.TerrainMask.value;
            walkableRegionsDictionary.Add((int) Mathf.Log(region.TerrainMask.value,2),region.terrainPenalty);
        }
        GridCreation();
    }

    public void ResetNodeValue()
    {
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                
                
                _grid[x,y].PathLeaghtAfterJump= Int32.MaxValue;
                _grid[x,y].PathLeaghtBeforeJump= Int32.MaxValue;
            }
        }
    }

    private void GridCreation()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;
            
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + nodeRadius) + Vector3.forward * (y * _nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unWalkableMask));
                int movementPenalty=0;
                
                if (walkable)
                {
                    Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, walkableMask))
                    {
                        walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                    }
                }
                
                _grid[x,y] = new Node(walkable,worldPoint, x,y, movementPenalty);
            }
        }
    }

    public bool OutOfBound(Vector2Int postion)
    {
        if (postion.y >= 0 && postion.y< _gridSizeY)
        {
            if (postion.x >= 0 && postion.x< _gridSizeX)
            {
                return false;
            }
        }
        return true;

    }
    
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        // for (int x = 0; x < node.GridX; x++)
        // {
        //     for (int y = 0; y < node.GridY; y++)
        //     {
        //         if (node.GridX-1 >= 0 )
        //         {
        //             neighbours.Add(_grid[node.GridX-1, node.GridY]);
        //         }
        //
        //         if (node.GridX+1 >=0)
        //         {
        //             neighbours.Add(_grid[node.GridX+1, node.GridY]);
        //         }
        //
        //         if (node.GridY-1 >= 0 )
        //         {
        //             neighbours.Add(_grid[node.GridX, node.GridY-1]);
        //         }
        //
        //         if (node.GridY+1>= 0)
        //         {
        //             neighbours.Add(_grid[node.GridX, node.GridY+1]);
        //         }
        //     }
        // }
        //TODO add while loop or for

        // for (int x = 0; x < node.GridX; x++)
        // {
        //     for (int y = 0; y < node.GridY; y++)
        //     {
        //         
        //     }
        // }
        // for (int x = Math.Max(node.GridX-1,0); x < node.GridX+2 && x< _gridSizeX; x++)
        // {
        //     for (int y = Math.Max(node.GridY-1,0); y < node.GridY+2&& y < _gridSizeY; y++)
        //     {
        //         neighbours.Add(_grid[x,y]);
        //     }
        // }

        
        
        if (node.GridY+1 >= 0 && node.GridY+1< _gridSizeY)
        {
            neighbours.Add(_grid[node.GridX, node.GridY+1]);
        }
        
        if (node.GridX+1 >= 0 && node.GridX+1< _gridSizeX)
        {
            neighbours.Add(_grid[node.GridX+1, node.GridY]);
        }
        
        if (node.GridY-1 >= 0 && node.GridY-1< _gridSizeY)
        {
            neighbours.Add(_grid[node.GridX, node.GridY-1]);
        }
        
        if (node.GridX-1 >= 0 && node.GridX-1< _gridSizeX)
        {
            neighbours.Add(_grid[node.GridX-1, node.GridY]);
        }

        
        
        // for (int x = -1; x <= 1; x++)
        // {
        //     for (int y = -1; y <= 1; y++)
        //     {
        //         if (x== 0 && y == 0)
        //         {
        //             continue;
        //         }
        //
        //         int checkX = node.GridX+x;
        //         int checkY = node.GridY + y;
        //         if (checkX>=0 && checkX< _gridSizeX&& checkY>= 0 && checkY< _gridSizeY)
        //         {
        //             neighbours.Add(_grid[checkX,checkY]);
        //         }
        //     }
        //     
        // }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp(percentX, 0, 1);
        percentY = Mathf.Clamp(percentY, 0, 1);

        int x = (int) ((_gridSizeX - 1) * percentX);
        int y = (int) ((_gridSizeY - 1) * percentY);
        return _grid[x, y];
    }
    

    public List<Node> Path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (_grid!= null)
        {
            foreach (Node node in _grid)
            {
                Gizmos.color = (node.Walkable)?Color.yellow : Color.red;

                if (Path != null)
                {
                    
                    if (Path.Contains(node))
                    {
                        Gizmos.color= Color.blue;
                    }
                }
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (_nodeDiameter-0.1f));
            }
        }
    }
    [Serializable]
    public class TerrainType
    {
        public LayerMask TerrainMask;
        public int terrainPenalty;
    }
}