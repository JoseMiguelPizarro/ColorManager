using UnityEditor;
using UnityEngine;

namespace RoJo.ColorManagement
{
	[CustomPropertyDrawer(typeof(MaterialSwatch))]
	public class SwatchPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var so = property.serializedObject;

			var colorProperty = property.FindPropertyRelative(nameof(MaterialSwatch.color));
			var previousColor = colorProperty.colorValue;

			EditorGUI.BeginChangeCheck();
			var materialsProperty = property.FindPropertyRelative(nameof(MaterialSwatch.materialReferences));

			var horizontalSpace = 10;
			var horizontalOffset = position.width / 3.0f;

			position.width /= 3.0f;

			var lineHeight = EditorGUIUtility.singleLineHeight;

			var color = EditorGUI.ColorField(new Rect(position.x, position.y, position.width, lineHeight), previousColor);

			if (EditorGUI.EndChangeCheck())
			{
				colorProperty.colorValue = color;
				so.ApplyModifiedProperties();
			}

			var materialsPosition = new Rect(position.x + horizontalOffset + horizontalSpace, position.y, horizontalOffset * 1.5f, lineHeight);

			if (GUI.Button(materialsPosition, "Add Material"))
			{
				materialsProperty.InsertArrayElementAtIndex(materialsProperty.arraySize);
				property.serializedObject.ApplyModifiedProperties();
			}

			EditorGUI.BeginChangeCheck();

			for (int i = 0; i < materialsProperty.arraySize; i++)
			{
				var materialP = materialsProperty.GetArrayElementAtIndex(i);

				materialsPosition.y += lineHeight;
				EditorGUI.PropertyField(materialsPosition, materialP);

				var removePosition = materialsPosition;
				removePosition.x += materialsPosition.width + horizontalSpace;
				removePosition.width = 0.5f * horizontalOffset - horizontalSpace * 2;

				if (GUI.Button(removePosition, "X"))
					materialsProperty.DeleteArrayElementAtIndex(i);
			}

			if (EditorGUI.EndChangeCheck())
				so.ApplyModifiedProperties();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var materials = property.FindPropertyRelative(nameof(MaterialSwatch.materialReferences));
			return (1 + materials.arraySize) * EditorGUIUtility.singleLineHeight;
		}
	}
}