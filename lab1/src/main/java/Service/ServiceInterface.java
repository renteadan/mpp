package Service;

import Domain.BaseEntity;

import java.util.Vector;

public interface ServiceInterface<E extends BaseEntity> {
  E find(int id);
  void delete(E entity);
  E update(E entity);
  E insert(E entity);
  Vector<E> findAll();
}
