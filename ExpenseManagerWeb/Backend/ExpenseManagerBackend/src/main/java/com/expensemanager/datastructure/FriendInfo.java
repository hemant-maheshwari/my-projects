package com.expensemanager.datastructure;

public class FriendInfo {

	private Integer userId;
	private String firstName;
	private String lastName;
	private double amount;
	
	public FriendInfo(Integer userId, String firstName, String lastName, double amount) {
		this.userId = userId;
		this.firstName = firstName;
		this.lastName = lastName;
		this.amount = amount;
	}

	public FriendInfo() {
	}
	
	public Integer getUserId() {
		return userId;
	}

	public void setUserId(Integer userId) {
		this.userId = userId;
	}

	public String getFirstName() {
		return firstName;
	}

	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}

	public String getLastName() {
		return lastName;
	}

	public void setLastName(String lastName) {
		this.lastName = lastName;
	}

	public double getAmount() {
		return amount;
	}

	public void setAmount(double amount) {
		this.amount = amount;
	}	

	
}
