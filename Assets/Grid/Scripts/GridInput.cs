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
        [SerializeField] SoGameObject NewGridObj;
        [SerializeField] SOEvents RayCastEvent;
        [SerializeField] GridObjectHolderSO CurrentObject;
        [SerializeField] SOTransform ObjectParent;

        Coroutine _raycastRotine;
        GridTile _lastHighlightedTile;
        GridObjectView _lastHighlightedObj;


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

            _raycastRotine = StartCoroutine(RayCasting());
        }

        private IEnumerator RayCasting()
        {
            while (Editing.Value)
            {   
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, RayCastLength, tileLayer))
                {
                    if (hit.collider.CompareTag("GridTile"))
                    {
                        if (_lastHighlightedObj != null)
                        {
                            _lastHighlightedObj.ShowFullView();
                            _lastHighlightedObj = null;
                        }
                        GridTile selectedTile = hit.collider.GetComponent<GridTile>();
                        if (selectedTile != _lastHighlightedTile)
                        {
                            if (_lastHighlightedTile != null)
                            {
                                _lastHighlightedTile.RemoveHighlight();
                                _lastHighlightedTile = null;
                            }
                            selectedTile.HighlightTile(1);
                            _lastHighlightedTile = selectedTile;
                            if (CurrentObject.GridObject != null && ObjectParent.Component != null)
                            {
                                CurrentObject.GridObject.CheckForLocation(selectedTile, ObjectParent.Component); 
                            }
                        }

                        Debug.Log("GridTile");
                        if (Input.GetMouseButtonDown(0) && CurrentObject.GridObject != null)
                        {
                            CurrentObject.GridObject.InstantiateObject();
                        }
                    }
                    else if (hit.collider.CompareTag("GridObj"))
                    {
                        if (_lastHighlightedTile != null)
                        {
                            _lastHighlightedTile.RemoveHighlight();
                            _lastHighlightedTile = null;
                        }
                        GridObjectView selectedObj = hit.collider.GetComponent<GridObjectView>();
                        Debug.Log(selectedObj);
                        Debug.Log(_lastHighlightedObj);
                        if (selectedObj != _lastHighlightedObj)
                        {
                            if (_lastHighlightedObj != null)
                            {
                                _lastHighlightedObj.ShowFullView();
                            }
                            NewGridObj.DestroyObject();
                            selectedObj.HighlightObject();
                            _lastHighlightedObj = selectedObj;
                        }
                        Debug.Log("GridObj");
                        if (Input.GetMouseButtonDown(1) && selectedObj != null)
                        {
                            selectedObj.RemoveObject();
                        }
                    }
                }
                yield return new WaitForSeconds(0.002f);
            }
            StopRayCasting();
        }

        private void StopRayCasting()
        {
            if (_raycastRotine != null)
            {
                StopCoroutine(_raycastRotine);
            }
        }
    }
}
