package com.simulation.reticlehandlersimulation.util;

import com.simulation.reticlehandlersimulation.model.Station;
import com.simulation.reticlehandlersimulation.model.Transition;
import java.io.File;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.logging.FileHandler;
import java.util.logging.Logger;
import java.util.logging.SimpleFormatter;

public class OutputLogWriter {

    private String outputLogFileName;  
    //private BufferedWriter bufferedWriter;
    private List<Station> stations;
    
    private final SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MM-dd-yyyy hh:mm:ss");
    private final String RETICLE_MOTION_STATUS_START = "START";
    //private final String RETICLE_MOTION_STATUS_END = "END";
    
    private Logger logger;
    
    public OutputLogWriter() {
        getStations();
        getOutputLogFileName();
        createBufferedWriter();
    }
    
    private void getStations(){
        StationsInventory stationsInventory = new StationsInventory();
        stations = stationsInventory.getAllStations();
    }
    
    private void createBufferedWriter(){
        try {
            String filename = "OutputLogs//"+outputLogFileName;
            File file = new File(filename);
            file.createNewFile();
            FileHandler fileHandler = new FileHandler(filename, true);
            logger = Logger.getLogger("output");
            logger.addHandler(fileHandler);
            SimpleFormatter simpleFormatter = new SimpleFormatter();
            fileHandler.setFormatter(simpleFormatter);
            //FileWriter fileWriter = new FileWriter(file);
            //bufferedWriter = new BufferedWriter(fileWriter);
        } catch (Exception e) {
            System.out.println("Error creating output log file.");
        }
    }
    
    private String getDateTimeStamp(){
        return simpleDateFormat.format(new Date());
    }
            
    private void getOutputLogFileName(){
        outputLogFileName = getDateTimeStamp()+".txt";
    }
    
    public void writeToLog(String reticleName, Transition transition, String motionCode){
        String content = getLogEntry(reticleName, transition, motionCode);
        try {
            logger.info(content);
            //bufferedWriter.write(content);
        } catch (Exception ex) {
            ex.printStackTrace();
            System.out.println("Error while writing to output log file.");
        }
    }
    
    private String getLogEntry(String reticleName, Transition transition, String motionCode){
        String content = getDateTimeStamp()+" ";
        if(motionCode.equals(RETICLE_MOTION_STATUS_START)){
            String stationName = getStationName(transition.getStartLocationX(), transition.getStartLocationY());
            content = content + reticleName + " started to move from "+ stationName;
        }
        else{
            String stationName = getStationName(transition.getFinalLocationX(), transition.getFinalLocationY());
            content = content + reticleName + " moved to "+ stationName;
        }
        return content;
    }
    
    private String getStationName(int x, int y){
        for(Station station: stations){
            if(station.getX()==x && station.getY()==y){
                return station.getName();
            }
        }
        return null;
    }
    
    private String getStationName(double x, double y){
        for(Station station: stations){
            if(station.getX()==(int)x && station.getY()==(int)y){
                return station.getName();
            }
        }
        return null;
    }
    
}
