using System.Collections.Generic;
using UnityEngine;

namespace RoJo.ColorManagement
{
	[CreateAssetMenu(menuName = "ColorManagement/MaterialPalette", fileName = "MaterialPalette")]
	public class MaterialPalette : ScriptableObject
	{
		public List<MaterialSwatch> swatches;

		public void Apply()
		{
			foreach (var swatch in swatches)
			{
				swatch.Apply();
			}
		}
	}
}