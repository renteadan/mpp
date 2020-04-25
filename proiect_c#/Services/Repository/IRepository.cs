using csharp.Domain;
using System.Collections.Generic;

public interface IRepository<E> where E : BaseEntity
{
	E Find(int id);

	void Delete(E entity);

	void Update(E entity);

	E Insert(E entity);

	List<E> FindAll();

	List<E> FindLastN(int n);
}