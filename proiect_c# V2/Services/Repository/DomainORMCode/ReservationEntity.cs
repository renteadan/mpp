using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Services.Repository.ygfxpsvi
{

	public partial class ReservationEntity
	{
		public ReservationEntity(Session session) : base(session) { }
		public override void AfterConstruction() { base.AfterConstruction(); }
	}

}
