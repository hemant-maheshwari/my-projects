package com.simulation.reticlehandlersimulation.model;

import java.awt.Color;
import java.util.ArrayList;
import java.util.List;

public class ReticlePointer {

    private String reticleName;
    private Color reticleColor;
    private double currentLocationX;
    private double currentLocationY;
    private List<Transition> transitions;
    private Transition activeTransition;
    private List<Transition> finishedTransitions;

    public ReticlePointer(String reticleName, Color reticleColor, double startLocationX, double startLocationY) {
        this.reticleName = reticleName;
        this.reticleColor = reticleColor;
        currentLocationX = startLocationX;
        currentLocationY = startLocationY;
        transitions = new ArrayList<>();
        finishedTransitions = new ArrayList<>();
    }

    public String getReticleName() {
        return reticleName;
    }

    public void setReticleName(String reticleName) {
        this.reticleName = reticleName;
    }

    public Color getReticleColor() {
        return reticleColor;
    }

    public void setReticleColor(Color reticleColor) {
        this.reticleColor = reticleColor;
    }

    public List<Transition> getTransitions() {
        return transitions;
    }

    public void setTransitions(List<Transition> transitions) {
        this.transitions = transitions;
    }

    public double getCurrentLocationX() {
        return currentLocationX;
    }

    public void setCurrentLocationX(double currentLocationX) {
        this.currentLocationX = currentLocationX;
    }

    public double getCurrentLocationY() {
        return currentLocationY;
    }

    public void setCurrentLocationY(double currentLocationY) {
        this.currentLocationY = currentLocationY;
    } 

    public Transition getActiveTransition() {
        return activeTransition;
    }

    public void setActiveTransition(Transition activeTransition) {
        this.activeTransition = activeTransition;
    }

    public List<Transition> getFinishedTransitions() {
        return finishedTransitions;
    }

    public void setFinishedTransitions(List<Transition> finishedTransitions) {
        this.finishedTransitions = finishedTransitions;
    }
    
}
