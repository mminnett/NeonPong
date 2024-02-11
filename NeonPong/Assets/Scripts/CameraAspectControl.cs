/*
 * Author: Matthew Minnett
 * Date: 11/6/2023
 * Desc: Adds border around camera view to match desired aspect (used notes from lecture)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))] // requires gameobject to have camera component attached
public class CameraAspectControl : MonoBehaviour
{
    public readonly float targetAspectRatio = 16f / 9f; // the aspect ratio desired

    private void Start()
    {
        StartCoroutine(UpdateAspectRatio()); // on start, start coroutine
    }

    /// <summary>
    /// Shows bars along top and bottom of screen to match desired aspect ratio
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateAspectRatio()
    {
        Camera cam = GetComponent<Camera>();

        while(true)
        {
            float currentAspectRatio = (float)Screen.width / (float)Screen.height;
            float scaleHeight = currentAspectRatio / targetAspectRatio;

            Rect rect = cam.rect;

            if(scaleHeight < 1f)
            {
                rect.width = 1f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1f - scaleHeight) / 2f;
                cam.rect = rect;
            }
            else
            {

                float scaleWidth = 1f / scaleHeight;
                rect.width = scaleWidth;
                rect.height = 1f;
                rect.x = (1 - scaleWidth) / 2f;
                rect.y = 0;
                cam.rect = rect;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
