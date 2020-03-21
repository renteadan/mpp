package Validator;

import Domain.BaseEntity;
import Errors.ValidationError;

public interface ValidatorInterface<E extends BaseEntity> {
  void Validate(E entity) throws ValidationError;
}
