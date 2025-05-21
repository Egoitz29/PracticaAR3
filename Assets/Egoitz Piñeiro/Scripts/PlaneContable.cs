using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneContable : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public TMP_Text horizontalPlaneText;
    public TMP_Text verticalPlaneText;

    void Update()
    {
        if (planeManager != null && horizontalPlaneText != null && verticalPlaneText != null)
        {
            int horizontalCount = 0;
            int verticalCount = 0;

            foreach (var plane in planeManager.trackables)
            {
                if (plane.alignment == PlaneAlignment.HorizontalUp || plane.alignment == PlaneAlignment.HorizontalDown)
                {
                    horizontalCount++;
                }
                else if (plane.alignment == PlaneAlignment.Vertical)
                {
                    verticalCount++;
                }
            }

            horizontalPlaneText.text = "Horizontales: " + horizontalCount;
            verticalPlaneText.text = "Verticales: " + verticalCount;
        }
    }
}
