using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shopping.Utils
{
    public static class GraphicUtils
    {
        public static List<RaycastResult> Raycast(GraphicRaycaster raycaster, EventSystem eventSystem, Vector2 position)
        {
            var eventData = new PointerEventData(eventSystem);
            eventData.position = position;
            var results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);

            return results;
        }
    }
}