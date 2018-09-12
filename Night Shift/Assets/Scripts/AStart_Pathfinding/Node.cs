using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {

    public bool walkable_;
    public Vector3 worldPosition_;
    public int gridX_, gridY_;
    int heapIndex;

    // Cost of the path from the start Node to this Node
    public int gCost;
    // Cost of the straight-line distance to the goal
    public int hCost;
    // Last Node used to get to this Node (used to track back the path later)
    public Node parent;


    // Constructor
    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY) {
        walkable_ = walkable;
        worldPosition_ = worldPos;
        gridX_ = gridX;
        gridY_ = gridY;
    }


    // Sum of gCost and hCost (cost to minimize)
    public int fCost {
        get { return gCost + hCost; }
    }


    // Public accessor
    public int HeapIndex {
        get { return heapIndex; }
        set { heapIndex = value; }
    }


    // Implement CompareTo for Node
    public int CompareTo(Node node) {
        // Compare fCost first (1 if (fCost - node.fCost) > 0, 0 if equal, or -1 if (fCost - node.fCost) < 0)
        int compare = fCost.CompareTo(node.fCost);
        //If same fCost, compare hCost
        if (compare == 0) {
            compare = hCost.CompareTo(node.hCost);
        }
        // Return minus compare to return 1 if current is lower
        return -compare;
    }
}
