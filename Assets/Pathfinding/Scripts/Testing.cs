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
    private List<GameObject> WPMarksList; //Lista de objetos que marcan el waypoint
    [SerializeField] private Transform scrollBar; //La scrollbar donde poner la plantilla de arriba y que sea scrolleable
    private List<Vector3> waypointList; //De pairs <x,y>
    private void Start() {
        pathfinding = new Pathfinding(X, Y);
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
        waypointList = new List<Vector3>();
        WPMarksList = new List<GameObject>();
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y,false);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    //Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            characterPathfinding.SetTargetPosition(mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            if(!pathfinding.GetNode(x,y).isWaypoint) pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }

        if (Input.GetKeyDown("w"))
        {
            //VISUAL
            //Vemos donde "clickó" el usuario para colocar un waypoint
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            //Debug.Log("Waypoint colocado en X:"+x+" Y:"+y);

            //pathfinding.GetNode(x, y).SetIsWaypoint(!pathfinding.GetNode(x, y).isWaypoint);

            //SYSTEM
            if (pathfinding.GetNode(x, y) != null && pathfinding.GetNode(x, y).isWalkable)
            {

          
            if(pathfinding.GetNode(x, y).isWaypoint)
            {
                pathfinding.GetNode(x, y).SetIsWaypoint(false);
                if (pathfinding.GetNode(x, y).marker != null) {
                    Destroy(pathfinding.GetNode(x, y).marker); 
                }
                Vector3 buscado = new Vector3(x * 10f, y * 10f);
                foreach (var w in waypointList){ //borramos el waypoint que tenga la misma direccion
                    if(w.x == buscado.x && w.y == buscado.y){
                        waypointList.Remove(w);
                    }
                }
            }
            else
            {   
 
                pathfinding.GetNode(x, y).SetIsWaypoint(true);
                GameObject wp = Instantiate(WaypointPrefab);
                WPMarksList.Add(wp);
                

                //wp.transform.SetParent(scrollBar);
                wp.transform.localScale = new Vector3(3, 3, 3);

                waypointList.Add(new Vector3(x * 10f, y * 10f));
                float Cellsize = pathfinding.GetGrid().GetCellSize();
                wp.GetComponent<Transform>().position = new Vector3(x * 10f, y * 10f) + (new Vector3(Cellsize, Cellsize) * 0.5f);
                pathfinding.GetNode(x, y).marker = wp;
            }
            }
            //wp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = waypointList.Count.ToString();
            //wp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (x/10f).ToString();
            //wp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (y/10f).ToString();

        }
    }

    public void CloseApp() {
        Application.Quit();
    }
    public void wayPointStart(){
        //Personaje
        characterPathfinding.SetTargetPosition(waypointList);
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
