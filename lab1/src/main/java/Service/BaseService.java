package Service;

import Domain.BaseEntity;
import Errors.SQLErrorNoEntityFound;
import Gateway.GatewayInterface;

import java.util.Vector;

public class BaseService<E extends BaseEntity> implements ServiceInterface<E> {

  private GatewayInterface<E> gateway;

  protected BaseService(GatewayInterface<E> gateway) {
    this.gateway = gateway;
  }
  @Override
  public E find(int id) throws SQLErrorNoEntityFound {
    return gateway.find(id);
  }

  @Override
  public void delete(E entity) {
    gateway.delete(entity);
  }

  @Override
  public E update(E entity) {
    return gateway.update(entity);
  }

  @Override
  public E insert(E entity) {
    return gateway.insert(entity);
  }

  @Override
  public Vector<E> findAll() {
    return gateway.findAll();
  }

  public Vector<E> findLastN(int n) {
    return gateway.findLastN(n);
  }
}
