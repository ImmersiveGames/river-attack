using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlink : MonoBehaviour {

    public float duration; // tempo para a compensação ocorrer
    public Color color1; // cor inicial para o blink
    public Color color2; // cor final para o blink

    /// <summary>
    /// Evento de piscar a tela
    /// </summary>
    /// <param name="dummy">apenas uma marcação falsa</param>
    public void BlinkCamera(Vector2 dummy)
    {
        StartCoroutine(BlinkCamera());
    }
    /// <summary>
    /// Faz a Camera piscar nas cores setadas na duração configurada
    /// </summary>
    /// <returns></returns>
    private IEnumerator BlinkCamera()
    {
        float nt = Time.time + duration * 20;
        do
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(color1, color2, t);
            yield return null;
        } while (nt > Time.time);
        Camera.main.backgroundColor = color1;
    }
}
