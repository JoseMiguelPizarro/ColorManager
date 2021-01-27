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

		public ColorManager colorManager;
	}

}