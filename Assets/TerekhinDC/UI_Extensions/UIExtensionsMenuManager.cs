using TerekhinDC.UI_Extensions.Components;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TerekhinDC.UI_Extensions
{
    [ExecuteInEditMode]
    public class UIExtensionsMenuManager : MonoBehaviour
    {
        [MenuItem("GameObject/UI Extensions/Editable Search DropDown")]
        private static void CreateEditableSearchDropDown()
        {
            var canvas = FindObjectOfType<Canvas>();
            
            if (canvas is null)
            {
                var go = new GameObject();
                canvas = go.AddComponent<Canvas>();
            }

            canvas.name = "Canvas";
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();

            var newform = EditableSearchDropDown.Create().transform.parent = canvas.transform;
            newform.name = "Editable Search DropDown";
        }
    }
}
