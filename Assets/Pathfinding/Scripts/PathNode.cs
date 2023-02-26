/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;
    public string W;
    public bool isWalkable;
    public bool isWaypoint;
    public PathNode cameFromNode;
    public GameObject marker;

    public PathNode(Grid<PathNode> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable= true;
        this.W = "";
        isWaypoint = false;
        
    }

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }
    public void SetIsWaypoint(bool isWaypoint)
    {   
        this.isWaypoint = isWaypoint;
        if(this.isWaypoint) { this.W = "W"; }
        else{this.W = "";}
        grid.TriggerGridObjectChanged(x, y);
    }
    public override string ToString() {
        return x + "," + y;
    }

}
