using System;

[Serializable]
public class SQLErrorNoEntityFound : Exception
{
	public SQLErrorNoEntityFound(string message) : base(message) { }
}