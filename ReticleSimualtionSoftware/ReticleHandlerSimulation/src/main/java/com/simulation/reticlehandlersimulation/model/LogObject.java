package com.simulation.reticlehandlersimulation.model;

import java.util.Date;

public class LogObject {

    private Date objectTime;
    private String objectType;
    private String data;

    public Date getObjectTime() {
        return objectTime;
    }

    public void setObjectTime(Date objectTime) {
        this.objectTime = objectTime;
    }

    public String getObjectType() {
        return objectType;
    }

    public void setObjectType(String objectType) {
        this.objectType = objectType;
    }

    public String getData() {
        return data;
    }

    public void setData(String data) {
        this.data = data;
    }
    
}
