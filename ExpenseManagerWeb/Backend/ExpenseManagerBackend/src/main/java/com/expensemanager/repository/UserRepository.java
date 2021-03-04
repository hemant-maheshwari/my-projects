package com.expensemanager.repository;

import org.springframework.data.repository.CrudRepository;

import com.expensemanager.model.User;

public interface UserRepository extends CrudRepository<User, Integer>{

}
