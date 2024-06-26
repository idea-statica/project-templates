﻿using CadApi;
using IdeaStatiCa.BimApi;
using IdeaStatiCa.BimApiLink.Importers;

namespace BimApiCadLink.Importers
{
	public abstract class BaseImporter<T> : IntIdentifierImporter<T> where T : IIdeaObject
	{
		protected ICadGeometryApi Model { get; }

		protected BaseImporter(ICadGeometryApi model)
		{
			Model = model;
		}
	}
}
