using System;

namespace csharp.Domain
{
	[Serializable]
	public class BaseEntity
	{
		public int Id { get; set; }

		public BaseEntity(int id)
		{
			this.Id = id;
		}

		public BaseEntity()
		{
		}
	}
}
