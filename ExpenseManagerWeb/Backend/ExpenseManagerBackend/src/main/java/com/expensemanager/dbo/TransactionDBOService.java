package com.expensemanager.dbo;

import java.util.List;

import org.springframework.stereotype.Service;

import com.expensemanager.database.DatabaseService;
import com.expensemanager.datastructure.AccountStatement;
import com.expensemanager.model.Transaction;

@Service
public class TransactionDBOService {

	private DatabaseService databaseService;
	
	public TransactionDBOService() {
		databaseService = new DatabaseService();
	}
	
	public List<Transaction> getAll(Integer userId){
		return databaseService.getAllTransactions(userId);
	}
	
	public AccountStatement getAccountStatement(Integer userId) {
		return databaseService.getAccountStatement(userId);
	}
	
}
