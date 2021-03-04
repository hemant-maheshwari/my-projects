package com.simulation.reticlehandlersimulation.model;

import java.util.Date;

public class Motion {
    
    private String location;
    private Date startTime;

    public Motion(String location, Date startTime) {
        this.location = location;
        this.startTime = startTime;
    }
    
    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public Date getStartTime() {
        return startTime;
    }

    public void setStartTime(Date startTime) {
        this.startTime = startTime;
    }
    
}
