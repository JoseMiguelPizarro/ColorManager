using System;
using UnityEngine;

namespace RoJo.ColorManagement
{
	[Serializable]
	public class MaterialPropertyReference
	{
		public Material material;
		public int propertyId;
		public string propertyName;
	}
}