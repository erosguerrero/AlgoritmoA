
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadricula<TObjeto> {

    public event EventHandler<ObjetoCambiadoEventArgs> ObjetoCambiado ;
    public class ObjetoCambiado EventArgs : EventArgs {
        public int x;
        public int y;
    }

    private int ancho;
    private int alto;
    private float tamanoCelda;
    private Vector3 origen;
    private Tobjeto[,] gridArray;

    public Grid(int ancho, int alto, float tamanoCelda, Vector3 origen, Func<Grid<Tobjeto>, int, int, Tobjeto> createGridObject) {
        this.ancho = ancho;
        this.alto = alto;
        this.tamanoCelda = tamanoCelda;
        this.origen = origen;

        gridArray = new Tobjeto[ancho, alto];

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        bool showDebug = false;
        if (showDebug) {
            TextMesh[,] debugTextArray = new TextMesh[ancho, alto];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(tamanoCelda, tamanoCelda) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, alto), GetWorldPosition(ancho, alto), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(ancho, 0), GetWorldPosition(ancho, alto), Color.white, 100f);

            ObjetoCambiado  += (object sender, ObjetoCambiado EventArgs eventArgs) => {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    public int Getancho() {
        return ancho;
    }

    public int Getalto() {
        return alto;
    }

    public float GettamanoCelda() {
        return tamanoCelda;
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * tamanoCelda + origen;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition - origen).x / tamanoCelda);
        y = Mathf.FloorToInt((worldPosition - origen).y / tamanoCelda);
    }

    public void SeTobjeto(int x, int y, Tobjeto value) {
        if (x >= 0 && y >= 0 && x < ancho && y < alto) {
            gridArray[x, y] = value;
            if (ObjetoCambiado  != null) ObjetoCambiado (this, new ObjetoCambiado EventArgs { x = x, y = y });
        }
    }

    public void TriggerGridObjectChanged(int x, int y) {
        if (ObjetoCambiado  != null) ObjetoCambiado (this, new ObjetoCambiado EventArgs { x = x, y = y });
    }

    public void SeTobjeto(Vector3 worldPosition, Tobjeto value) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SeTobjeto(x, y, value);
    }

    public Tobjeto GeTobjeto(int x, int y) {
        if (x >= 0 && y >= 0 && x < ancho && y < alto) {
            return gridArray[x, y];
        } else {
            return default(Tobjeto);
        }
    }

    public Tobjeto GeTobjeto(Vector3 worldPosition) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GeTobjeto(x, y);
    }

}


