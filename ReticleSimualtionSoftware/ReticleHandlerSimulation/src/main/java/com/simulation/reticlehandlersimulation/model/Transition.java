package com.simulation.reticlehandlersimulation.model;

public class Transition {
    
    private int startLocationX;
    private int startLocationY;
    private double initialLocationX;
    private double initialLocationY;
    private double velocityX;
    private double velocityY;
    private double finalLocationX;
    private double finalLocationY;
    private long travelTime;

    public Transition() {
    }
    
    public Transition(double initialLocationX, double initialLocationY, double finalLocationX, double finalLocationY, long travelTime) {
        this.initialLocationX = initialLocationX;
        this.initialLocationY = initialLocationY;
        this.finalLocationX = finalLocationX;
        this.finalLocationY = finalLocationY;
        this.travelTime = travelTime;
        startLocationX = (int)initialLocationX;
        startLocationY = (int)initialLocationY;
        calculateVelocities();
    }
    
    private void calculateVelocities(){
        if(travelTime == 0){
            velocityX = 0;
            velocityY = 0;
        }
        else{
            velocityX = (double)(finalLocationX - initialLocationX)/(travelTime/1000.0);
            velocityY = (double)(finalLocationY - initialLocationY)/(travelTime/1000.0);
        }
    }

    public double getInitialLocationX() {
        return initialLocationX;
    }

    public void setInitialLocationX(double initialLocationX) {
        this.initialLocationX = initialLocationX;
    }

    public double getInitialLocationY() {
        return initialLocationY;
    }

    public void setInitialLocationY(double initialLocationY) {
        this.initialLocationY = initialLocationY;
    }

    public double getVelocityX() {
        return velocityX;
    }

    public void setVelocityX(double velocityX) {
        this.velocityX = velocityX;
    }

    public double getVelocityY() {
        return velocityY;
    }

    public void setVelocityY(double velocityY) {
        this.velocityY = velocityY;
    }

    public double getFinalLocationX() {
        return finalLocationX;
    }

    public void setFinalLocationX(double finalLocationX) {
        this.finalLocationX = finalLocationX;
    }

    public double getFinalLocationY() {
        return finalLocationY;
    }

    public void setFinalLocationY(double finalLocationY) {
        this.finalLocationY = finalLocationY;
    }

    public long getTravelTime() {
        return travelTime;
    }

    public void setTravelTime(long travelTime) {
        this.travelTime = travelTime;
    }

    public int getStartLocationX() {
        return startLocationX;
    }

    public void setStartLocationX(int startLocationX) {
        this.startLocationX = startLocationX;
    }

    public int getStartLocationY() {
        return startLocationY;
    }

    public void setStartLocationY(int startLocationY) {
        this.startLocationY = startLocationY;
    }

    
    
    
    
}
