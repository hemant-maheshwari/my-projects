package com.expensemanager.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.expensemanager.dbo.UserDBOService;
import com.expensemanager.model.User;
import com.expensemanager.repository.UserRepository;

@Service
public class UserService {

	@Autowired
	UserRepository userRepository;
	@Autowired
	UserDBOService userDBOService;

	public User create(User user) {
		return userRepository.save(user);
	}
	
	public User find(User user) {
		int userId = userDBOService.findUserId(user);
		if(userId == 0) {
			user.setId(userId);
			return user;
		}
		else {
			return findByID(userId);
		}
	}
	
	public User find(String email) {
		int userId = userDBOService.findUserId(email);
		if(userId == 0) {
			return null;
		}
		else {
			return findByID(userId);
		}
	}
	
	public void update(User user) {
		userRepository.save(user);
	}
	
	public List<User> findAll(String hint){
		return userDBOService.findAll(hint);
	}
	
	private User findByID(Integer userId) {
		return userRepository.findById(userId).get(); 
	}
		
}
