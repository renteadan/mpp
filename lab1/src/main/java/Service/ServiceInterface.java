package Service;

import Domain.BaseEntity;
import Errors.SQLErrorNoEntityFound;
import Errors.ValidationError;

import java.util.Vector;

public interface ServiceInterface<E extends BaseEntity> {
  E find(int id) throws SQLErrorNoEntityFound;
  void delete(E entity);
  E update(E entity) throws ValidationError;
  E insert(E entity) throws ValidationError;
  Vector<E> findAll();
}
