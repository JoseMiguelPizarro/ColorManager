using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoJo.ColorManagement
{
	[Serializable]
	public class MaterialSwatch
	{
		public Color color;
		public List<MaterialPropertyReference> materialReferences = new List<MaterialPropertyReference>();

		public void Apply()
		{
			foreach (var materialReference in materialReferences)
			{
				if (materialReference.material != null)
				{
					materialReference.material?.SetColor(materialReference.propertyName, color);
				}
			}
		}
	}
}