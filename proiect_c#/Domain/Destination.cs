using System;


namespace csharp.Domain
{
	[Serializable]
	public class Destination : BaseEntity
	{
		public string Name { get; set; }

		public Destination(int id, string name) : base(id)
		{
			Name = name;
		}

		public Destination(string name)
		{
			Name = name;
		}

		public Destination()
		{
		}

		public override bool Equals(object obj)
		{
			return obj is Destination destination &&
						 Id == destination.Id;
		}

		public override int GetHashCode()
		{
			return 539060726 + Id.GetHashCode();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}