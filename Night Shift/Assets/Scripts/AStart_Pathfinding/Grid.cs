using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public bool onlyShowPathGizmos;
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public List<Node> path;

    private readonly Vector3 centerOfMap = new Vector3(0, 0, 0);

    // Public accessor
    public int MaxSize {
        get { return gridSizeX * gridSizeY; }
    }


    // Executed first on play
    void Start() {
        nodeDiameter = nodeRadius * 2;
        
        // Calculate the grid size of the algorithm map
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }


    // Create the grid used for the pathfinding
    void CreateGrid() {
        // Initialise a 2d array of node
        grid = new Node[gridSizeX, gridSizeY];
        // Find the bottom-left position of the grid in game-view
        Vector3 worldBottomLeft = centerOfMap - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;

        // For every position in the grid...
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                // Find the position of the node in game-view by adding incremential distances to the bottom-left position
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                // Check if the node is located in a walkable area
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                // Add the Node to the grid
                grid[x, y] = new Node(walkable, worldPoint, x, y);

            }
        }
    }


    // Get the list of neighbours of a given Node
    public List<Node> GetNeighbours(Node node) {
        // Initiate the list
        List<Node> neighbours = new List<Node>();

        // For every neighbour of the node (3 - 8 possible neighbours)
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                // Excluding self
                if (x == 0 && y == 0)
                    continue;

                // Get the neighbour position 
                int checkX = node.gridX_ + x;
                int checkY = node.gridY_ + y;

                // If the neighbour position is not out of the map, add it to the list of neighbour
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        // Return the list
        return neighbours;
    }


    // Return the node located at the given world position
    public Node NodeFromWorldPoint(Vector3 worldPosition) {

        // Get percent position (grid has center at (0, 0, 0), so add half size to make bottom-left at (0, 0, 0) and divide by size to get percent)
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        // Clamp the percent between 0 and 1 to avoid any errors if out of range
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // Find the grid position by multiplying the percent to the grid size, then round to int
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        // Return the node at the given position
        return grid[x, y];
    }


    // Draw items in the editor view (not visible in game view)
    void OnDrawGizmos() {
        // Draw wireframe around the grid map
        Gizmos.DrawWireCube(centerOfMap, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        // If bool is checked
        if (onlyShowPathGizmos) {
            // If path not null
            if (path != null) {
                // For each Node in the path...
                foreach (Node node in path) {
                    // Use color black and draw Node
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(node.worldPosition_, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
        // If bool unchecked
        else {
            // If the grid is not empty
            if (grid != null) {
                // Get the Node the player is on
                Node playerNode = NodeFromWorldPoint(player.position);
                // For each Node in the grid
                foreach (Node node in grid) {
                    // If the Node is walkable, use white. Else, use red
                    Gizmos.color = (node.walkable_) ? Color.white : Color.red;

                    // If player is on the node, use cyan
                    if (playerNode == node)
                        Gizmos.color = Color.cyan;
                    // If a path exist, and the Node is in the path, use black
                    if (path != null)
                        if (path.Contains(node))
                            Gizmos.color = Color.black;

                    // Draw every Node as a cube of the corresponding color
                    Gizmos.DrawCube(node.worldPosition_, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}
