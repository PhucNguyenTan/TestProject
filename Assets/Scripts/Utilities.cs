using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3[] GetCorners(this RectTransform rect)
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);
        return corners;
    }

    public static float MaxY(this RectTransform rect)
    {
        return rect.GetCorners()[1].y;
    }

    public static float MinY(this RectTransform rect)
    {
        return rect.GetCorners()[0].y;
    }

    public static float MaxX(this RectTransform rect)
    {
        return rect.GetCorners()[2].x;
    }

    public static float MinX(this RectTransform rect)
    {
        return rect.GetCorners()[0].x;
    }

    public static void SetTopAnchor(RectTransform rect)
    {
        float width = rect.rect.width;
        float height = rect.rect.height;

        rect.anchorMax = new Vector2(0.5f, 1f);
        rect.anchorMin = new Vector2(0.5f, 1f);
        rect.pivot = new Vector2(0.5f, 1f);

        rect.sizeDelta = new Vector2(width, height);
    }

    public static void SetTopLeftAnchor(RectTransform rect)
    {
        float width = rect.rect.width;
        float height = rect.rect.height;

        rect.anchorMax = new Vector2(0f, 1f);
        rect.anchorMin = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);

        rect.sizeDelta = new Vector2(width, height);

    }




}
