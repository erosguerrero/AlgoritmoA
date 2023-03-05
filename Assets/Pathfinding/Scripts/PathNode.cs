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
    public int costTho;
    public string W;
    public bool isWalkable;
    public bool isWaypoint;
    public PathNode cameFromNode;
    public GameObject marker;

    public PathNode(Grid<PathNode> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.W = "";
        int prob = Random.Range(0, 100);
        if (prob < 10)
        {
            this.costTho = 9999;
        }
        else if (prob <20) {
            this.costTho = Random.Range(100,1000);//9999;
        } else {
            this.costTho = 0;
        }
        isWaypoint = false;
        int number = Random.Range(0,100);
        
        if(number < 15 && this.x > 0 && this.y > 0){
            this.isWalkable = false;
        }else{
            this.isWalkable = true;
        }
        
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
