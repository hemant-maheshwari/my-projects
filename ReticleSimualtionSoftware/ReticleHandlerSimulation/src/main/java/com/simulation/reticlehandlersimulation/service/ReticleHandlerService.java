package com.simulation.reticlehandlersimulation.service;

import com.simulation.reticlehandlersimulation.constant.LogEntryType;
import com.simulation.reticlehandlersimulation.model.LogObject;
import com.simulation.reticlehandlersimulation.model.Motion;
import com.simulation.reticlehandlersimulation.model.Path;
import com.simulation.reticlehandlersimulation.model.Reticle;
import com.simulation.reticlehandlersimulation.util.LogReader;
import java.io.File;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ReticleHandlerService {

    private final LogHandlerService logHandlerService;
    private final LogReader logReader;
    private final String[] logData;
    private final String REGEX = "'";
    private final String MOTION_CODE = "1234";
    private List<Reticle> reticles;
    
    public ReticleHandlerService(File logFile) {
        logHandlerService = new LogHandlerService();
        logReader = new LogReader(logFile);
        logData = logReader.readFile();
        reticles = new ArrayList<>();
    }
    
    public List<Reticle> getReticles(){
        getReticlesFromLogObject();
        updateReticlePaths();
        return reticles;
    }
    
    private void getReticlesFromLogObject(){
        List<LogObject> logObjects = logHandlerService.getLogObjectFromLogData(logData);
        for(LogObject logObject: logObjects){
            if(logObject.getObjectType().equals(LogEntryType.SdrRequest.name())){
                if(containsMotionCode(logObject.getData())){
                    updateReticles(logObject);
                }
            }
        }
    }
    
    private boolean containsMotionCode(String logEntryString){
        return logEntryString.contains(MOTION_CODE);
    }
    
    private void updateReticles(LogObject logObject){
        String[] reticleData = parseReticleDataFromEntryLog(logObject.getData());
        String reticleName = reticleData[1];
        String stationName = reticleData[3];
        addUpdateReticle(reticleName, stationName, logObject.getObjectTime());
    }
    
    private void addUpdateReticle(String reticleName, String stationName, Date motionTime){
        Reticle reticle;
        if(findReticleIndex(reticleName)==-1){
            reticle = new Reticle(reticleName);
            reticles.add(reticle);
        }
        else{
            int reticleIndex = findReticleIndex(reticleName);
            reticle = reticles.get(reticleIndex);
        }
        Motion motion = new Motion(stationName, motionTime);
        reticle.getMotions().add(motion);
    }
    
    private int findReticleIndex(String reticleName){
        for(int i=0; i<reticles.size(); i++){
            if(reticles.get(i).getName().equals(reticleName)){
                return i;
            }
        }
        return -1;
    }
    
    private String[] parseReticleDataFromEntryLog(String reticleData){
        String[] strArr = reticleData.split(REGEX);
        return strArr;
    }
    
    private void updateReticlePaths(){
        for(Reticle reticle: reticles){
            updatePath(reticle);
        }
    }
    
    private void updatePath(Reticle reticle){
        List<Path> paths = new ArrayList<>();
        List<Motion> motions = reticle.getMotions();
        if(motions.size()>0){
            for(int i=0; i<motions.size()-1; i++){
                Path path = new Path(motions.get(i).getLocation(), motions.get(i+1).getLocation(), getTimeDiff(motions.get(i+1).getStartTime(), motions.get(i).getStartTime()));
                paths.add(path);
            }
        }
        reticle.setPaths(paths);
    }
    
    private long getTimeDiff(Date date1, Date date2){
        return date1.getTime()-date2.getTime();
    }
    
}
