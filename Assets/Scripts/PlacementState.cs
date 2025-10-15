using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectsDatabase database;
    GridData rugData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;

    // Define rug IDs here
    private int[] rugIDs = { 15, 16, 17 };

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          ObjectsDatabase database,
                          GridData rugData,
                          GridData furnitureData,
                          ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.rugData = rugData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                database.objectsData[selectedObjectIndex].Prefab,
                database.objectsData[selectedObjectIndex].Size);
        }
        else
            throw new System.Exception($"No object with ID {iD}");
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (!placementValidity)
            return;

        int index = objectPlacer.PlaceObject(
            database.objectsData[selectedObjectIndex].Prefab,
            grid.CellToWorld(gridPosition));

        int id = database.objectsData[selectedObjectIndex].ID;
        bool isRug = rugIDs.Contains(id);

        GridData selectedData = isRug ? rugData : furnitureData;

        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            id,
            index);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        int id = database.objectsData[selectedObjectIndex].ID;
        bool isRug = rugIDs.Contains(id);
        Vector2Int size = database.objectsData[selectedObjectIndex].Size;

        if (isRug)
        {
            // Rugs can only be placed if no rug already occupies the area
            return rugData.CanPlaceObjectAt(gridPosition, size);
        }
        else
        {
            // Furniture can be placed if no other furniture is there
            return furnitureData.CanPlaceObjectAt(gridPosition, size);
        }
    }



    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}
