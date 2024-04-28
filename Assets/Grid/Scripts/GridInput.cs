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

        private Coroutine RaycastRotine;

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
                    GridTile selectedTile = hit.collider.GetComponent<GridTile>();
                    Debug.Log(selectedTile.xIndex + "," + selectedTile.yIndex);

                    // Perform additional actions as needed
                }

                yield return new WaitForSeconds(0.01f);
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
