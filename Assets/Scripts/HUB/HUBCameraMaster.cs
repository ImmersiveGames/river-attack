using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HUBCameraMaster : Singleton<HUBCameraMaster> {

    [SerializeField]
    private Renderer spriteMapMundi;

    public Vector3 CamFixInBounds(Vector3 posOriginal)
    {
        Vector2 pontCamUpRight = (Vector2)posOriginal + (Utils.Tools.CamSize / 2); // ponto direito da camera
        Vector2 pontCamDownLeft = (Vector2)posOriginal - (Utils.Tools.CamSize / 2); // ponto esquerda da camera
        Vector2 bondMin = spriteMapMundi.bounds.min;
        Vector2 bondMax = spriteMapMundi.bounds.max;
        float x = (bondMin.x > pontCamDownLeft.x) ? posOriginal.x - (pontCamDownLeft.x - bondMin.x) : (bondMax.x < pontCamUpRight.x) ? posOriginal.x - (pontCamUpRight.x - bondMax.x) : posOriginal.x;
        float y = (bondMin.y > pontCamDownLeft.y) ? posOriginal.y - (pontCamDownLeft.y - bondMin.y) : (bondMax.y < pontCamUpRight.y) ? posOriginal.y - (pontCamUpRight.y - bondMax.y) : posOriginal.y;

        return new Vector3(x, y, transform.position.z);
    }

    public IEnumerator MoveCam(Vector3 pointTo, float time, bool setBounds = false)
    {
        if (setBounds)
            pointTo = CamFixInBounds(pointTo);
        if (time <= 0)
            transform.position = pointTo;
        else
        {
            while (transform.position != pointTo)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointTo, time * Time.deltaTime);
                yield return null;
            }
        }       
        yield return null;
    }
    protected override void OnDestroy() { }
}