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
using CodeMonkey.Utils;
using CodeMonkey;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Testing : MonoBehaviour {
    
    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfinding pathfinding;
    private int X=20, Y=10;

    //WAYPOINT SYSTEM
    [SerializeField] private GameObject WaypointPrefab; //Plantilla de las columnas de los prefabs con su info
    [SerializeField] private Transform scrollBar; //La scrollbar donde poner la plantilla de arriba y que sea scrolleable
    private List<Vector3> waypointList; //De pairs <x,y>
    private void Start() {
        pathfinding = new Pathfinding(X, Y);
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
        waypointList = new List<Vector3>();
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            characterPathfinding.SetTargetPosition(mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }

        if (Input.GetKeyDown("w"))
        {
            //Vemos donde "clickó" el usuario para colocar un waypoint
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            Debug.Log("Waypoint colocado en X:"+x+" Y:"+y);
            //TODO WAYPOING SYSTEM QUEUE

            GameObject wp = Instantiate(WaypointPrefab);
            wp.transform.SetParent(scrollBar);
            wp.transform.localScale= Vector3.one;
            waypointList.Add(new Vector3(x*10f, y*10f));
            wp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = waypointList.Count.ToString();
            wp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (x/10f).ToString();
            wp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (y/10f).ToString();
        }
    }

    public void wayPointStart(){
        characterPathfinding.SetTargetPosition(waypointList);
        waypointList.Clear();

    }
    public void changeGrid(int W, int H)
    {
        if (W <= 0) W = X;
        if (H <= 0) H = Y;
        X = W;
        Y = H;
        pathfinding = new Pathfinding(W, H);
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        Debug.Log(pathfinding.GetGrid().GetWidth() == W);

        pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }
}
