package com.expensemanager.dbo;

import java.util.List;

import org.springframework.stereotype.Service;

import com.expensemanager.database.DatabaseService;
import com.expensemanager.datastructure.FriendInfo;

@Service
public class FriendDBOService {

	private DatabaseService databaseService;
	
	public FriendDBOService() {
		databaseService = new DatabaseService();
	}
	
	public List<FriendInfo> getAllFriendsInfo(Integer userId){
		return databaseService.getAllFriendsInfo(userId);
	}
	
}
