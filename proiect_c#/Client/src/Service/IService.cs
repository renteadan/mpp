
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IService<E> where E: BaseEntity {
  E Find(int id);
  void Delete(E entity);
  void Update(E entity);
  E Insert(E entity);
  List<E> FindAll();
}
