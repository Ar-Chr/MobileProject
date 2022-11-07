using UnityEngine;
using UnityEditor;

namespace Ball.LevelManagement
{
    [CustomPropertyDrawer(typeof(SceneByAsset))]
    public class ScenePickerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SceneAsset oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.FindPropertyRelative("ScenePath").stringValue);

            var newScene = EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false) as SceneAsset;

            property.FindPropertyRelative("ScenePath").stringValue = AssetDatabase.GetAssetPath(newScene);
            property.FindPropertyRelative("SceneName").stringValue = newScene?.name;

            EditorGUI.EndProperty();
        }
    }
}