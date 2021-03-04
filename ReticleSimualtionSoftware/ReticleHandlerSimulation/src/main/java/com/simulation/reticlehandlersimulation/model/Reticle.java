package com.simulation.reticlehandlersimulation.model;

import java.util.ArrayList;
import java.util.List;

public class Reticle {
    
    private String name;
    private List<Motion> motions;
    private List<Path> paths;
    
    public Reticle(String name) {
        this.name = name;
        motions = new ArrayList<>();
        paths = new ArrayList<>();
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Motion> getMotions() {
        return motions;
    }

    public void setMotions(List<Motion> motions) {
        this.motions = motions;
    }

    public List<Path> getPaths() {
        return paths;
    }

    public void setPaths(List<Path> paths) {
        this.paths = paths;
    }
    
    
    
}
