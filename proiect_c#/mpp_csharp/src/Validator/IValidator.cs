
public interface IValidator<E> where E: BaseEntity {
  void Validate(E entity);
}
