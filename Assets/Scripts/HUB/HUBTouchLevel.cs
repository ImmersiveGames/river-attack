using UnityEngine;

public class HUBTouchLevel : MonoBehaviour {
    [SerializeField]
    private float timeMoveCam;
    private HUBIconMaster hubIconLevel;

    private void Update()
    {
        CheckIconFocus();
    }
    private void CheckIconFocus()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //Debug.Log("Atingi Icone" + hit.collider.name + " " + hit.point);
            if (hit.collider != null && hit.collider.GetComponent<HUBIconMaster>())
            {
                //Debug.Log("Atingi Icone" + hit.collider.name + " " + hit.point);
                StartCoroutine(HUBCameraMaster.Instance.MoveCam(hit.point, timeMoveCam, true));
                hubIconLevel = hit.collider.GetComponent<HUBIconMaster>();
                GameManager.Instance.SetActualLevel(hubIconLevel.MyLevel);
                hit.collider.GetComponent<HUBIconSelect>().IconeOnFocus(hubIconLevel.MyLevel, hit.point);
            }
        }
    }
}
