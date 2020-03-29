
using System.Collections.Generic;
using System.Threading.Tasks;

public class BaseService<E,G>: IService<E> where E: BaseEntity where G: IRepository<E> {

  protected readonly G repository;
  private IValidator<E> validator;

  protected BaseService(G repository, IValidator<E> validator) {
    this.repository = repository;
    this.validator = validator;
  }

  public  E Find(int id) {
    return  repository.Find(id);
  }

  public void Delete(E entity) {
    repository.Delete(entity);
  }

  public void Update(E entity) {
    validator.Validate(entity);
    repository.Update(entity);
  }

  public E Insert(E entity) {
    validator.Validate(entity);
    return repository.Insert(entity);
  }

  public List<E> FindAll() {
    return repository.FindAll();
  }

  public List<E> FindLastN(int n) {
    return repository.FindLastN(n);
  }

  public  int Count() {
    var list =  repository.FindAll();
    return list.Count;
  }
}
