package com.expensemanager.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.expensemanager.datastructure.FriendInfo;
import com.expensemanager.dbo.FriendDBOService;
import com.expensemanager.model.Friend;
import com.expensemanager.repository.FriendRepository;

@Service
public class FriendService {

	@Autowired
	FriendRepository friendRepository;
	@Autowired
	FriendDBOService friendDBOService;
	
	public List<FriendInfo> findAll(Integer userId){
		return friendDBOService.getAllFriendsInfo(userId);
	}
	
	public Friend create(Friend friend) {
		return friendRepository.save(friend);
	}
	
}
