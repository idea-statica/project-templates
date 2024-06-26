﻿using BimApiCadLink.BimApi;
using BimApiCadLink.Utils;
using CadApi;
using IdeaStatiCa.BimApi;

namespace BimApiCadLink.Importers
{
	public class PlateImporter : BaseImporter<IIdeaPlate>
	{
		public PlateImporter(ICadGeometryApi model): base(model)
		{
		}

		public override IIdeaPlate Create(int id)
		{
			var item = Model.GetPlate(id);
			
			if (item != null)
			{
				var p = new Plate(item.Id);
				if (FillPlateInstance(item, p))
				{
					return p;
				}
				else
				{
					return null;
				}
			}

			return null;
		}

		private bool FillPlateInstance(CadPlate cadplate, Plate plate) 
		{
			plate.MaterialNo = Model.GetMaterialByName(cadplate.Material).Id;

			CadPlane3D plane = cadplate.CadOutline2D.Plane;

			var localCoordinateSystem = plane.ToIdea();
			
			plate.LocalCoordinateSystem = localCoordinateSystem;

			plate.Thickness = cadplate.Thickness;
			plate.Geometry = cadplate.CadOutline2D.ToIdea();
			plate.OriginNo = Utils.PointTranslator.GetPointId(cadplate.CadOutline2D.Plane.Origin);

			return true;
		}
	}
}
