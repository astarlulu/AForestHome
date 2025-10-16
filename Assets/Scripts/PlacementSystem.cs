using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class PlacementSystem : MonoBehaviour
{
   
    
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabase database;
    

    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    private GridData rugData, furnitureData;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    private int currentRotation = 0; // Stores rotation in degrees (0, 90, 180, 270)


    IBuildingState buildingState;

    private void Start()
    {

        inputManager = FindFirstObjectByType<InputManager>(); // ✅ Fix
        rugData = new();
        furnitureData = new();
        StopPlacement(); // ✅ Now safe to call

    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID,
                                           grid,
                                           preview,
                                           database,
                                           rugData,
                                           furnitureData,
                                           objectPlacer);
       
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;

    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid,
                                          preview,
                                          rugData,
                                          furnitureData,
                                          objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        buildingState.OnAction(gridPosition);

    }

    //private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    //{
    //    int id = database.objectsData[selectedObjectIndex].ID;
    //    int[] rugIDs = { 15, 16, 17 };
    //    bool isRug = rugIDs.Contains(id);
    //    Vector2Int size = database.objectsData[selectedObjectIndex].Size;

    //    if (isRug)
    //    {
    //        // Rugs can be placed only if no rug already occupies the cell
    //        return rugData.CanPlaceObjectAt(gridPosition, size);
    //    }
    //    else
    //    {
    //        // Furniture can be placed if there's no furniture already there,
    //        // but rugs underneath are allowed
    //        bool canPlaceOnFurniture = furnitureData.CanPlaceObjectAt(gridPosition, size);
    //        return canPlaceOnFurniture;
    //    }
    //}


    private void StopPlacement()
    {
        if (buildingState == null)
            return;
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    private void Update()
    {
        if (buildingState == null)
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }

        // 🔹 Add this block for rotation input
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentRotation = (currentRotation + 90) % 360;
            if (buildingState is PlacementState placementState)
            {
                placementState.RotatePreview(currentRotation);
            }
        }
    }


}
