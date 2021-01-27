using UnityEngine;
using UnityEditor;

namespace RoJo.ColorManagement
{
	[CustomEditor(typeof(ColorManager))]
	public class ColorManagerEditor : Editor
	{
		ColorManager colorManager;
		SerializedObject currentPaletteSO;

		private void OnEnable()
		{
			colorManager = target as ColorManager;
			InitializeSwatches();

			Undo.undoRedoPerformed += UndoHandler;
		}

		private void InitializeSwatches()
		{
			if (colorManager.currentPalette)
			{
				foreach (var swatch in colorManager.currentPalette.swatches)
				{
					swatch.colorManager = colorManager;
				}
			}
		}

		private void OnDisable()
		{
			Undo.undoRedoPerformed -= UndoHandler;
		}

		private void UndoHandler()
		{
			colorManager.ApplyColors();
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();

			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ColorManager.currentPalette)));

			if (EditorGUI.EndChangeCheck())
			{
				InitializeSwatches();
				serializedObject.ApplyModifiedProperties();
			}

			var palette = colorManager.currentPalette;

			if (palette != null)
			{
				using (new EditorGUILayout.VerticalScope("Box"))
				{
					EditorGUI.BeginChangeCheck();

					EditorGUILayout.LabelField("Swatches", EditorStyles.boldLabel);
					EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

					currentPaletteSO = new SerializedObject(palette);
					var property = currentPaletteSO.GetIterator();
					property.Next(true);

					var swatches = currentPaletteSO.FindProperty(nameof(MaterialPalette.swatches));

					for (int i = 0; i < swatches.arraySize; i++)
					{
						DrawSwatch(swatches, i);

						EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
					}

					if (GUILayout.Button("Add swatch"))
					{
						var index = swatches.arraySize;
						swatches.InsertArrayElementAtIndex(index);
						currentPaletteSO.ApplyModifiedProperties();

						var swatch = palette.swatches[index];
						swatch.colorManager = colorManager;
					}

					if (EditorGUI.EndChangeCheck())
					{
						colorManager.ApplyColors();
					}
				}
			}
		}

		private void DrawSwatch(SerializedProperty swatches, int index)
		{
			var swatch = swatches.GetArrayElementAtIndex(index);
			EditorGUILayout.PropertyField(swatch);
			if (GUILayout.Button("Remove"))
			{
				swatches.DeleteArrayElementAtIndex(index);

				currentPaletteSO.ApplyModifiedProperties();
			}
		}
	}
}

