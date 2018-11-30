﻿using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LayerMaskReference))]
public class LayerMaskReferencePD : PropertyDrawer {
    public static GUIStyle popupStyle;

    private readonly string[] popupOptions = { "Use Constant", "Use Variable" };

    int size = 0;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        size = 0;
        if (popupStyle == null) {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        // Get properties
        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("value");
        SerializedProperty variable = property.FindPropertyRelative("variable");
        SerializedProperty gameEvent = property.FindPropertyRelative("myEvent");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);
        useConstant.boolValue = result == 0;

        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width / 2, position.height),
            useConstant.boolValue ? constantValue : variable, GUIContent.none);

        if (EditorGUI.EndChangeCheck()) {
            property.serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.PropertyField(new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height), gameEvent, GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}