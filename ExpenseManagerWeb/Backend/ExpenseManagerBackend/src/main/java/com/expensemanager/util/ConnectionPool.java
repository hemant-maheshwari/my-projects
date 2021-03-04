package com.expensemanager.util;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;


public class ConnectionPool implements Runnable{

	private static int initialConnectionCount = 5;
	private List<Connection> availableConnections = new ArrayList<>();
	private List<Connection> usedConnections = new ArrayList<>();
	
	//---------------DEV-----------------------------------
	private static final String URL = "jdbc:mysql://aaedszor9s2hyx.czrw0hltunvb.us-east-2.rds.amazonaws.com:3306/ebdb";
	private static final String USERNAME = "expense";
	private static final String PASSWORD = "password";
	
	//---------------LOCALHOST------------------------------
	//private static final String URL = "jdbc:mysql://localhost:3306/expense_manager";
	//private static final String USERNAME = "root";
	//private static final String PASSWORD = "Universal0";
	
	private static final Logger log = LogManager.getLogger(ConnectionPool.class);
	
	static{
		log.debug("Loading oracle driver...");
	
		try {
		    Class.forName("com.mysql.jdbc.Driver");
			log.debug("Driver loaded!");
		} catch (ClassNotFoundException e) {
		    throw new IllegalStateException("Cannot find the driver in the classpath!", e);
		}
	}
		
	/**
	 * kick off the monitoring thread
	 */
	private void init() {
		ExecutorService executor = Executors.newSingleThreadExecutor();
		executor.execute(this);
 	}
	
	/**
	 * Create a ConnectionPool
	 *
	 * The default path to the override.properties file is ./opt.  
	 * If yours is different call the overloaded ConnectionPool(String path) constructor
	 * 
	 * @throws SQLException
	 */
	public ConnectionPool() throws SQLException {
		for (int cnt = 0; cnt < initialConnectionCount; cnt++) {
			availableConnections.add(getConnection());
		}
		log.debug("Starting off with " + initialConnectionCount + " connections");
		init();
	}

	
	private Connection getConnection(){
		try {
			return DriverManager.getConnection(URL, USERNAME, PASSWORD);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}
	
	/**
	 * Checkout a connection.  Check it back in when you're finished.
	 * 
	 * @return db connection. If none left in the pool, one is created.
	 * @throws SQLException
	 */

	public synchronized Connection checkout() throws SQLException {
		Connection newConnxn = null;

		if (availableConnections.size() == 0) {
			log.debug("Used everything in the pool, create one more..");
			newConnxn = getConnection();
			usedConnections.add(newConnxn);
		} else {
			newConnxn = (Connection) availableConnections.get(availableConnections.size() - 1);
			availableConnections.remove(newConnxn);
			usedConnections.add(newConnxn);
		}
		log.debug("Got a conn, current available: " + availableCount());
		return newConnxn;
	}

	/**
	 * Return your connection to the pool here
	 * 
	 * @param c
	 */
	public synchronized void checkin(Connection c) {
		if (c != null) {
			usedConnections.remove(c);
			availableConnections.add(c);
			log.debug("Returned a conn, current available: " + availableCount());

		}
	}

	/**
	 * Check the current count of connections
	 * 
	 * @return
	 */
	public int availableCount() {
		return availableConnections.size();
	}

	/**
	 * Every minute check for stale connections & replace them.  Check for connection count in the pool
	 * exceeding the properties specified count & remove.  Needs to be public to implement Runnable but no 
	 * utility for the client to call directly. 
	 * 
	 */
	public void run() {
		try {
			while (true) {
				synchronized (this) {
					while (availableConnections.size() > initialConnectionCount) {
		 				Connection c = (Connection) availableConnections.get(availableConnections.size() - 1);
						availableConnections.remove(c);

						log.debug("removing extra connection");
						c.close();
					}
					
					for (int i = 0; i < availableConnections.size(); i++) {
		 				Connection c = (Connection) availableConnections.get(i);
		 				if(c.isClosed() || !c.isValid(4000)) {
		 					availableConnections.remove(c);
		 					availableConnections.add(getConnection());
		 					log.debug("replacing 'stale' connection");
		 						
		 				}
 					}
				}

				log.debug("CLEANUP : Available Connections : " + availableCount());

				// Now sleep for 1 min
				Thread.sleep(60000 * 1);
			}
		} catch (SQLException sqle) {
			sqle.printStackTrace();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void forceCleanup() {
		try {
			while (availableConnections.size() > 0) {
				Connection c = (Connection) availableConnections.get(availableConnections.size() - 1);
				availableConnections.remove(c);

				log.debug("closing connection " + c);
				c.close();
			}
		} catch (SQLException sqle) {
			sqle.printStackTrace();
		} catch (Exception e) {
			e.printStackTrace();
		}
		log.debug("All connections closed.");
	}

	
}
