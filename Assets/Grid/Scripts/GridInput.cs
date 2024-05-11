using UnityEngine;
using System.Collections;
using ProjectCore.Events;
using ProjectCore.Variables;

namespace ProjectCore.Grid
{
    public class GridInput : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] LayerMask tileLayer;
        [SerializeField] float RayCastLength;
        [SerializeField] SOBool Editing;
        [SerializeField] SOEvents RayCastEvent;
        [SerializeField] GridObjectHolderSO CurrentObject;
        [SerializeField] SOTransform ObjectParent;

        private Coroutine RaycastRotine;
        private GridTile lastHighlightedTile;

        private void OnEnable()
        {
            RayCastEvent.Handler += StartRayCasting;
        }

        private void OnDisable()
        {
            RayCastEvent.Handler -= StartRayCasting;
        }

        private void StartRayCasting()
        {

            RaycastRotine = StartCoroutine(RayCasting());
        }

        private IEnumerator RayCasting()
        {
            while (Editing.Value)
            {   
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Perform raycasting
                if (Physics.Raycast(ray, out hit, RayCastLength, tileLayer))
                {
                    //Debug.Log("Here");
                    GridTile selectedTile = hit.collider.GetComponent<GridTile>();
                    if (selectedTile != lastHighlightedTile)
                    {
                        // Remove highlight from the last highlighted tile
                        if (lastHighlightedTile != null)
                        {
                            lastHighlightedTile.RemoveHighlight();
                        }

                        // Highlight the selected tile
                        selectedTile.HighlightTile();
                        lastHighlightedTile = selectedTile;
                        if (CurrentObject.GridObject != null && ObjectParent.Component != null)
                        { CurrentObject.GridObject.CheckForLocation(selectedTile, ObjectParent.Component); }
                    }

                    if (Input.GetMouseButtonDown(0) && CurrentObject.GridObject != null)
                    {
                        CurrentObject.GridObject.InstantiateObject();
                    }
                }
                yield return new WaitForSeconds(0.005f);
            }
            StopRayCasting();
        }

        private void StopRayCasting()
        {
            if (RaycastRotine != null)
            {
                StopCoroutine(RaycastRotine);
            }
        }
    }
}
