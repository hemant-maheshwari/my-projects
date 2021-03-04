package com.expensemanager.database;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.List;

import com.expensemanager.datastructure.AccountStatement;
import com.expensemanager.datastructure.FriendInfo;
import com.expensemanager.model.Share;
import com.expensemanager.model.Transaction;
import com.expensemanager.model.User;
import com.expensemanager.util.ConnectionPool;

public class DatabaseService {
	
	private static ConnectionPool connectionPool;
	
	public DatabaseService(){
		try {
			getInstance();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	public synchronized static ConnectionPool getInstance() throws SQLException {
		if(connectionPool==null)
		{
			connectionPool = new ConnectionPool();
		}
		return connectionPool;
	}
	
	public List<Transaction> getAllTransactions(Integer userId){
		List<Transaction> transactions = new ArrayList<>();
		Connection connection = null;
		try {
			String query = "{CALL GET_ALL_TRANSACTION(?)}";
			ResultSet resultSet;
			connection = connectionPool.checkout();
			CallableStatement statement = connection.prepareCall(query);
			statement.setInt(1, userId);
			resultSet = statement.executeQuery();
			while(resultSet.next()) {
				transactions.add(getTransaction(resultSet));
			}
		} catch (Exception e) {
			System.out.println("Error getting transactions for user "+userId);
		}finally {
			connectionPool.checkin(connection);
		}
		return transactions;
	}
	
	private Transaction getTransaction(ResultSet resultSet) {
		Transaction transaction = new Transaction();
		try {
			transaction.setId(resultSet.getInt(1));
			transaction.setAmount(resultSet.getDouble(2));
			transaction.setDateCreated(resultSet.getString(3));
			transaction.setDateUpdated(resultSet.getString(4));
			transaction.setTitle(resultSet.getString(5));
			transaction.setType(resultSet.getString(6));
			transaction.setUserId(resultSet.getInt(7));
		} catch (SQLException e) {
			System.out.println("Error reading from transaction table");
		}
		transaction.setShares(getShares(transaction.getId()));
		return transaction;
	}
	
	private List<Share> getShares(Integer transactionId){
		List<Share> shares = new ArrayList<>();
		Connection connection = null;
		try {
			String query = "{CALL GET_ALL_SHARE(?)}";
			ResultSet resultSet;
			connection = connectionPool.checkout();
			CallableStatement statement = connection.prepareCall(query);
			statement.setInt(1, transactionId);
			resultSet = statement.executeQuery();
			while(resultSet.next()) {
				shares.add(getShare(resultSet));
			}
		} catch (Exception e) {
			System.out.println("Error getting share for transaction "+transactionId);
		}finally {
			connectionPool.checkin(connection);
		}
		return shares;
	}
	
	private Share getShare(ResultSet resultSet) {
		Share share = new Share();
		try {
			share.setId(resultSet.getInt(1));
			share.setPartnerId(resultSet.getInt(2));
		} catch (SQLException e) {
			System.out.println("Error reading from share table");
		}
		return share;
	}
	
	public int findUserId(String email) {
		int userId = 0;
		Connection connection = null;
		try {
			String query = "{CALL GET_USER_ID_FROM_EMAIL(?,?)}";
			connection = connectionPool.checkout();
			CallableStatement statement = connection.prepareCall(query);
			statement.setString(1, email);
			statement.registerOutParameter(2, Types.INTEGER);
			statement.execute();
			userId = statement.getInt(2);
		} catch (Exception e) {
			System.out.println("Error getting user id for email "+email);
		}finally {
			connectionPool.checkin(connection);
		}
		return userId;
	}
	
	public int findUserId(User user) {
		int userId = 0;
		Connection connection = null;
		try {
			String query = "{CALL GET_USER_ID(?,?,?)}";
			connection = connectionPool.checkout();
			CallableStatement statement = connection.prepareCall(query);
			statement.setString(1, user.getUsername());
			statement.setString(2, user.getPassword());
			statement.registerOutParameter(3, Types.INTEGER);
			statement.execute();
			userId = statement.getInt(3);
		} catch (Exception e) {
			System.out.println("Error getting user id for username "+user.getUsername());
		}finally {
			connectionPool.checkin(connection);
		}
		return userId;
	}
	
	public List<User> getAllUsers(String hint){
		List<User> users = new ArrayList<>();
		Connection connection = null;
		try {
			String query = "{CALL GET_ALL_USERS(?)}";
			ResultSet resultSet;
			connection = connectionPool.checkout();
			CallableStatement statement = connection.prepareCall(query);
			statement.setString(1, hint);
			resultSet = statement.executeQuery();
			while(resultSet.next()) {
				users.add(getUser(resultSet));
			}
		} catch (Exception e) {
			System.out.println("Error getting users for hint "+hint);
		}finally {
			connectionPool.checkin(connection);
		}
		return users;
	}
	
	private User getUser(ResultSet resultSet) {
		User user = new User();
		try {
			user.setId(resultSet.getInt(1));
			user.setEmail(resultSet.getString(2));
			user.setFirstName(resultSet.getString(3));
			user.setLastName(resultSet.getString(4));
			user.setPassword(resultSet.getString(5));
			user.setPhone(resultSet.getString(6));
			user.setUsername(resultSet.getString(7));
		} catch (SQLException e) {
			System.out.println("Error reading from user table");
		}
		return user;
	}
	
	public List<FriendInfo> getAllFriendsInfo(Integer userId){
		List<FriendInfo> friendInfos = new ArrayList<>();
		Connection connection = null;
		try {
			String query = "{CALL GET_FRIENDS_INFO(?)}";
			ResultSet resultSet;
			connection = connectionPool.checkout();
			CallableStatement statement = connection.prepareCall(query);
			statement.setInt(1,userId);
			resultSet = statement.executeQuery();
			while(resultSet.next()) {
				friendInfos.add(getFriendInfo(resultSet));
			}
		} catch (Exception e) {
			System.out.println("Error getting friends info for user "+userId);
		}finally {
			connectionPool.checkin(connection);
		}
		updateAmount(userId, friendInfos);
		return friendInfos;
	}
	
	private FriendInfo getFriendInfo(ResultSet resultSet) {
		FriendInfo friendInfo = new FriendInfo();
		try {
			friendInfo.setUserId(resultSet.getInt(1));
			friendInfo.setAmount(resultSet.getDouble(4));
			friendInfo.setFirstName(resultSet.getString(2));
			friendInfo.setLastName(resultSet.getString(3));
		} catch (Exception e) {
			System.out.println("Error reading from friend info store procedure");
		}
		return friendInfo;
	}
	
	private void updateAmount(Integer userId, List<FriendInfo> friendInfos) {
		List<Transaction> transactions = getAllTransactions(userId);
		for(Transaction transaction: transactions) {
			if(transaction.getShares().size()>0) {
				double amount = transaction.getAmount()/(transaction.getShares().size()+1);
				for(Share share: transaction.getShares()) {
					FriendInfo friend = friendInfos.stream().filter(p -> p.getUserId().equals(share.getPartnerId())).findFirst().get();
					friend.setAmount(friend.getAmount()+amount);
				}
			}
		}
		for(FriendInfo friend: friendInfos) {
			double owedAmount = getOwedAmount(friend.getUserId(), userId);
			friend.setAmount(friend.getAmount()-owedAmount);
		}
	}
	
	private double getOwedAmount(Integer userId,Integer friendsId) {
		double amount = 0;
		List<Transaction> transactions = getAllTransactions(userId);
		for(Transaction transaction: transactions) {
			if(transaction.getShares().size()>0) {
				if(transaction.getShares().stream().filter(p -> p.getPartnerId().equals(friendsId)).findFirst().isPresent()) {
					amount = amount + transaction.getAmount()/(transaction.getShares().size()+1);
				}
			}
		}
		return amount;
	}
	
	public AccountStatement getAccountStatement(Integer userId) {
		AccountStatement accountStatement = new AccountStatement();
		Connection connection = null;
		try {
			String query = "{CALL GET_ACCOUNT_STATEMENT(?,?,?)}";
			connection = connectionPool.checkout();
			CallableStatement statement = connection.prepareCall(query);
			statement.setInt(1, userId);
			statement.registerOutParameter(2, Types.DOUBLE);
			statement.registerOutParameter(3, Types.DOUBLE);
			statement.execute();
			accountStatement.setIncome(statement.getDouble(2));
			accountStatement.setExpense(statement.getDouble(3));
		} catch (Exception e) {
			System.out.println("Error getting account statement for user "+userId);
		}finally {
			connectionPool.checkin(connection);
		}
		return accountStatement;
	}

}
