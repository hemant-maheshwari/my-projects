package com.expensemanager.repository;

import org.springframework.data.repository.CrudRepository;

import com.expensemanager.model.Transaction;

public interface TransactionRepository extends  CrudRepository<Transaction, Integer>{

}
