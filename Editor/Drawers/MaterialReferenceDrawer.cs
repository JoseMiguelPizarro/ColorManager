using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace RoJo.ColorManagement
{
	[CustomPropertyDrawer(typeof(MaterialPropertyReference))]
	public class MaterialReferenceDrawer : PropertyDrawer
	{
		private static List<string> colorProperties = new List<string>();

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			colorProperties.Clear();
			EditorGUI.BeginChangeCheck();
			var materialSP = property.FindPropertyRelative(nameof(MaterialPropertyReference.material));
			position.width *= 0.5f;
			materialSP.objectReferenceValue = EditorGUI.ObjectField(position, materialSP.objectReferenceValue, typeof(Material), allowSceneObjects: false);

			var material = materialSP.objectReferenceValue as Material;

			if (material != null)
			{
				var shader = material.shader;
				var propertyCount = shader.GetPropertyCount();

				for (int i = 0; i < propertyCount; i++)
				{
					var propertyType = shader.GetPropertyType(i);

					if (propertyType == ShaderPropertyType.Color)
					{
						var propertyName = shader.GetPropertyName(i);
						colorProperties.Add(propertyName);
					}
				}

				var propertyNameSP = property.FindPropertyRelative(nameof(MaterialPropertyReference.propertyName));

				position.x += position.width;

				var propertyIndex = colorProperties.IndexOf(propertyNameSP.stringValue);

				EditorGUI.BeginChangeCheck();
				propertyIndex = EditorGUI.Popup(position, propertyIndex, colorProperties.ToArray());

				if (EditorGUI.EndChangeCheck())
				{
					if (propertyIndex >= 0)
					{
						propertyNameSP.stringValue = colorProperties[propertyIndex];
					}

					property.serializedObject.ApplyModifiedProperties();
				}
			}


			if (EditorGUI.EndChangeCheck())
			{
				property.serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
