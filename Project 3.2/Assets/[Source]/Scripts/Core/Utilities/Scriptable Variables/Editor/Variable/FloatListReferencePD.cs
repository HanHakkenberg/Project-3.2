using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatListReference))]
public class FloatListReferencePD : PropertyDrawer {
    public static GUIStyle popupStyle;

    private readonly string[] popupOptions = { "Use Constant", "Use Variable" };

    float size = 0;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        if(popupStyle == null) {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        // Get properties
        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
        SerializedProperty value = property.FindPropertyRelative("value");
        SerializedProperty variable = property.FindPropertyRelative("variable");

        //Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);
        useConstant.boolValue = result == 0;

        if(useConstant.boolValue == true) {
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, position.height), value, GUIContent.none, true);
            size = EditorGUI.GetPropertyHeight(value, GUIContent.none);
        }
        else {
            Rect nonConstant = position;
            nonConstant.xMin = buttonRect.xMax;
            EditorGUI.PropertyField(new Rect(position.x + 20, position.y, position.width - 20, position.height), variable, GUIContent.none, true);
            size = EditorGUI.GetPropertyHeight(variable, GUIContent.none);
        }


        if(EditorGUI.EndChangeCheck()) {
            property.serializedObject.ApplyModifiedProperties();
        }

        //EditorGUI.PropertyField(new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height), myEvent, GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return size;
    }
}