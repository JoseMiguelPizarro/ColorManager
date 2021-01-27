using UnityEngine;

namespace RoJo.ColorManagement
{
	public class ColorManager : MonoBehaviour
	{
		public MaterialPalette currentPalette;

		public void ApplyColors()
		{
			foreach (var swatch in currentPalette.swatches)
			{
				ApplySwatch(swatch);
			}
		}

		public void ApplySwatch(MaterialSwatch swatch)
		{
			foreach (var materialReference in swatch.materialReferences)
			{
				if (materialReference.material != null)
				{
					materialReference.material?.SetColor(materialReference.propertyName, swatch.color);
				}
			}
		}
	} 
}
