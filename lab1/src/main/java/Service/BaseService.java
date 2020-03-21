package Service;

import Domain.BaseEntity;
import Errors.SQLErrorNoEntityFound;
import Errors.ValidationError;
import Gateway.GatewayInterface;
import Validator.ValidatorInterface;

import java.util.Vector;

public class BaseService<E extends BaseEntity, G extends GatewayInterface<E>> implements ServiceInterface<E> {

  protected G gateway;
  private ValidatorInterface<E> validator;

  protected BaseService(G gateway, ValidatorInterface<E> validator) {
    this.gateway = gateway;
    this.validator = validator;
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
  public E update(E entity) throws ValidationError {
    validator.Validate(entity);
    return gateway.update(entity);
  }

  @Override
  public E insert(E entity) throws ValidationError {
    validator.Validate(entity);
    return gateway.insert(entity);
  }

  @Override
  public Vector<E> findAll() {
    return gateway.findAll();
  }

  public Vector<E> findLastN(int n) {
    return gateway.findLastN(n);
  }

  public int count() {
    return gateway.findAll().size();
  }
}
