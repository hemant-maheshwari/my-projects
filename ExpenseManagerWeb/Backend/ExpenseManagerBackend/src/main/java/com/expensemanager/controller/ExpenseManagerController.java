package com.expensemanager.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.expensemanager.datastructure.AccountStatement;
import com.expensemanager.datastructure.FriendInfo;
import com.expensemanager.model.Friend;
import com.expensemanager.model.Transaction;
import com.expensemanager.model.User;
import com.expensemanager.service.FriendService;
import com.expensemanager.service.TransactionService;
import com.expensemanager.service.UserService;

@RestController
@CrossOrigin("*")
@PreAuthorize("hasRole('ROLE_USER')")
public class ExpenseManagerController {

	@Autowired
	UserService userService;
	@Autowired
	FriendService friendService;
	@Autowired
	TransactionService transactionService;
	
	@PostMapping("api/v1/createAccount")
	public void createUserAccount(@RequestBody User user) {
		user = verifyUser(user);
		if(user.getId() == 0) {
			user = userService.create(user);
		}
	}
	
	@PostMapping("api/v1/verify")
	public User verifyUser(@RequestBody User user) {
		return userService.find(user);
	}
	
	@PostMapping("api/v1/forgot")
	public User forgotPassword(@RequestBody String email) {
		return userService.find(email);
	}
	
	@PostMapping("api/v1/update")
	public void update(@RequestBody User user) {
		userService.update(user);
	}
	
	@GetMapping("api/v1/getAllFriends/{userId}")
	public List<FriendInfo> getAllFriends(@PathVariable("userId") Integer userId) {
		return friendService.findAll(userId);
	}
	
	@PostMapping("api/v1/createTransaction")
	public void createTransaction(@RequestBody Transaction transaction) {
		transactionService.create(transaction);
	}
	
	@GetMapping("api/v1/getAllTransactions/{userId}")
	public List<Transaction> getAllTransactions(@PathVariable("userId") Integer userId) {
		return transactionService.findAll(userId);
	}
	
	@GetMapping("api/v1/getAllUsers/{hint}")
	public List<User> getAllUsers(@PathVariable("hint") String hint) {
		return userService.findAll(hint);
	}
	
	@PostMapping("api/v1/addFriend")
	public void createFriend(@RequestBody Friend friend) {
		friendService.create(friend);
	}
	
	@GetMapping("api/v1/getStatement/{userId}")
	public AccountStatement getAllUsers(@PathVariable("userId") Integer userId) {
		return transactionService.getAccountStatement(userId);
	}
	
}
