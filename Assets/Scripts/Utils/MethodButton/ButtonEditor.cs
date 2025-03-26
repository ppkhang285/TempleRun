using UnityEditor;
using UnityEngine;
using System.Reflection;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var targetObject = target as MonoBehaviour;
        var methods = targetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(ButtonAttribute), true);
            if (attributes.Length > 0)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(targetObject, null);
                }
            }
        }
    }
}