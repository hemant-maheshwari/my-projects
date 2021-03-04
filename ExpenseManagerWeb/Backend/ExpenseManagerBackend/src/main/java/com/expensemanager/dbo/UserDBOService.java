package com.expensemanager.dbo;

import java.util.List;

import org.springframework.stereotype.Service;

import com.expensemanager.database.DatabaseService;
import com.expensemanager.model.User;

@Service
public class UserDBOService {

	private DatabaseService databaseService;
	
	public UserDBOService() {
		databaseService = new DatabaseService();
	}
	
	public int findUserId(User user) {
		return databaseService.findUserId(user);
	}
	
	public int findUserId(String email) {
		return databaseService.findUserId(email);		
	}
	
	public List<User> findAll(String hint){
		return databaseService.getAllUsers(hint);
	}
	
}
