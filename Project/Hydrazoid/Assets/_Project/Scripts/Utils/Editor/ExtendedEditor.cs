using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hydrazoid.Extensions
{
    public class ExtendedEditor : Editor
    {
        private static GUIContent _deleteButtonContent = new GUIContent("-", "delete");
        private static GUIContent _addButtonContent = new GUIContent("+", "add element");
        private static GUILayoutOption _miniButtonWidth = GUILayout.Width(20f);

        protected void DrawProperties(SerializedProperty property, bool drawChildren)
        {
            string lastPropPath = string.Empty;
            foreach (SerializedProperty prop in property)
            {
                if (prop.isArray && prop.propertyType == SerializedPropertyType.Generic)
                {
                    EditorGUILayout.BeginHorizontal();
                    prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, prop.displayName);
                    EditorGUILayout.EndHorizontal();

                    if (prop.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        DrawProperties(prop, drawChildren);
                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(lastPropPath) && prop.propertyPath.Contains(lastPropPath)) { continue; }
                    lastPropPath = prop.propertyPath;
                    EditorGUILayout.PropertyField(prop, drawChildren);
                }
            }
        }

        protected void DrawProperty(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property);
        }

        protected static void ShowArray(SerializedProperty list)
        {
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            SerializedProperty size = list.FindPropertyRelative("Array.size");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(list.displayName);
            EditorGUILayout.PropertyField(size);
            EditorGUILayout.EndHorizontal();

            if (size.hasMultipleDifferentValues)
            {
                EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
            }
            else
            {
                ShowItems(list);
            }

            EditorGUI.indentLevel -= 1;
        }

        private static void ShowItems(SerializedProperty list)
        {
            if (list.arraySize == 0 && GUILayout.Button(_addButtonContent, EditorStyles.miniButton))
            {
                list.arraySize += 1;
            }

            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
                ShowButtons(list, i);
                EditorGUILayout.EndHorizontal();
            }
        }

        private static void ShowButtons(SerializedProperty list, int index)
        {
            if (GUILayout.Button(_addButtonContent, EditorStyles.miniButtonMid, _miniButtonWidth))
            {
                list.InsertArrayElementAtIndex(index);
            }

            if (GUILayout.Button(_deleteButtonContent, EditorStyles.miniButtonRight, _miniButtonWidth))
            {
                int oldSize = list.arraySize;
                list.DeleteArrayElementAtIndex(index);
                if (list.arraySize == oldSize)
                {
                    list.DeleteArrayElementAtIndex(index);
                }
            }
        }

    }
}