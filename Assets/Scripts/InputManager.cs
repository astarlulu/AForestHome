using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;


public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    public event Action OnClicked, OnExit; //confirm player has clicked the LeftMouseButton 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke(); // Call the action event 
        if(Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();   // Player can escape to unclick an object
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject(); //Return us true/false if we are hovering over UI.

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
