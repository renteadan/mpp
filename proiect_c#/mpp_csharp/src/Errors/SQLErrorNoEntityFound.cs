using System;

public class SQLErrorNoEntityFound: Exception
{
	public SQLErrorNoEntityFound(string message) : base(message) { }
}