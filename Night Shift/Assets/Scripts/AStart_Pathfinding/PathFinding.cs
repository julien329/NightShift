using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PathFinding : MonoBehaviour {
    
    Grid grid;


    // Executed before Start() at play
    void Awake() {
        grid = GetComponent<Grid>();
    }


    // Executed on every frame
    void Update() {
    }


    // Find the shorthest path between two position
	public List<Node> FindPath(Vector3 startPos, Vector3 targetPos) {
        // Initiate StopWatch and start counting
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Find the corresponding Nodes of the given positons
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (!targetNode.walkable_)
        {
            return new List<Node>();
        }

        // Create a heap of available Node (open) and a HashSet of used Node (closed)
        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        // While the openSet is not empty
        while(openSet.Count > 0) {
            // Get the first Node in the heap (lowest cost) and add it to closedSet (used)
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            // If the current Node is the target Node
            if(currentNode == targetNode) {
                // Stop counting and print elapsed time in console
                sw.Stop();
                //print("Path found: " + sw.ElapsedMilliseconds + " ms");

                // Retrace used path and return

                return RetracePath(startNode, targetNode);
            }

            // For each Node neighbour to the current Node...
            foreach(Node neighbour in grid.GetNeighbours(currentNode)) {
                // If Node is not walkable or already used (in closedSet), go to next Node
                if (!neighbour.walkable_ || closedSet.Contains(neighbour))
                    continue;

                // Calculate gCost from current to neighbour
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                // If new cost is lower than existing one or if neighbour not in openSet
                if (!openSet.Contains(neighbour) || newMovementCostToNeighbour < neighbour.gCost) {
                    // Update gcost to new gCost, update hCost with distance to target and set parent
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    // If Node not in openSet, add it
                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                    // Else, update the item in the heap with new cost and position
                    else
                        openSet.UpdateItem(neighbour);
                }
            }
        }
        // No path was found, return empty list
        return new List<Node>();
    }


    // Get Nodes used to create the shortest path
    private List<Node> RetracePath(Node startNode, Node endNode) {
        // Initiate list to store the Nodes and set currentNode to the target Node (endNode)
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        // While the current node is not the startNode
        while(currentNode != startNode) {
            // Add the Node to the path list and update current to its parent
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        // Reverse path so it is in the proper order, and send result to grid.path
        path.Reverse();
        grid.path = path;
        return path;
    }


    // Get the distance between two Nodes
    int GetDistance(Node nodeA, Node nodeB) {
        // Calculate the absolute distance in X and Y between the two Nodes
        int distX = Mathf.Abs(nodeA.gridX_ - nodeB.gridX_);
        int distY = Mathf.Abs(nodeA.gridY_ - nodeB.gridY_);

        // If x greater than y : (need x-y straight lines (cost 10) and y diagonals (cost 14))
        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        // If y greater than x : (need y-x straight lines (cost 10) and x diagonals (cost 14))
        return 14 * distX + 10 * (distY - distX);
    }
}
