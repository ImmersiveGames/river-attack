using UnityEngine;
using UnityEngine.EventSystems;

public class HUBDragCamera : MonoBehaviour {

    [SerializeField]
    private float panSpeed;
    [SerializeField]
    private float m_dragThreshold;

    public bool CamDragging { get; private set; }
    private Vector2 oldPos;
    private Vector2 panOrigin;
    
    private float dragThreshold;

    private void Update()
    {
        DragCamera();
    }

    private void DragCamera()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            dragThreshold = m_dragThreshold;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) return;
            //Debug.Log(hit.collider);
            CamDragging = true;
            oldPos = Camera.main.transform.position;
            panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (CamDragging && Input.GetMouseButton(0))
        {
            dragThreshold -= Time.deltaTime;
            if (dragThreshold < 0)
            {
                HUBMaster.Instance.hubPause = true;
                HUBMaster.Instance.CallEventOnUnselectMission();
                Vector2 pos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;
                Vector3 nposition = HUBCameraMaster.Instance.CamFixInBounds(oldPos + -pos * panSpeed);
                Camera.main.transform.position = nposition;
            }
        }
        if (CamDragging && Input.GetMouseButtonUp(0))
        {
            dragThreshold = m_dragThreshold;
            CamDragging = false;
            HUBMaster.Instance.hubPause = false;
            HUBMaster.Instance.CallEventOnSelectMission(GameManager.Instance.actualLevel);
        }
    }
}
