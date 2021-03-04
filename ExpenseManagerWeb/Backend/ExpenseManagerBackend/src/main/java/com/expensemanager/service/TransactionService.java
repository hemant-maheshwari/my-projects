package com.expensemanager.service;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.expensemanager.datastructure.AccountStatement;
import com.expensemanager.dbo.TransactionDBOService;
import com.expensemanager.model.Share;
import com.expensemanager.model.Transaction;
import com.expensemanager.repository.ShareRepository;
import com.expensemanager.repository.TransactionRepository;

@Service
public class TransactionService {

	@Autowired
	TransactionRepository transactionRepository;
	@Autowired
	ShareRepository shareRepository;
	@Autowired
	TransactionDBOService transactionDBOService;
	
	private final String DATE_FORMAT = "MMM dd, yy";
	private final SimpleDateFormat simpleDateFormat = new SimpleDateFormat(DATE_FORMAT);
	
	public Transaction create(Transaction transaction) {
		String currentDate = getCurrentDate();
		transaction.setDateCreated(currentDate);
		transaction.setDateUpdated(currentDate);
		transaction = transactionRepository.save(transaction);
		for(Share share: transaction.getShares()) {
			share.setTransaction(transaction);
			shareRepository.save(share);
		}
		return transaction;
	}
	
	public List<Transaction> findAll(Integer userId){
		return transactionDBOService.getAll(userId);
	}
	
	public AccountStatement getAccountStatement(Integer userId) {
		return transactionDBOService.getAccountStatement(userId);
	}
	
	private String getCurrentDate() {
		return simpleDateFormat.format(new Date());
	}
	
}
