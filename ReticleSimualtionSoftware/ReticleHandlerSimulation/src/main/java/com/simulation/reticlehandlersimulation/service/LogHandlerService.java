package com.simulation.reticlehandlersimulation.service;

import com.simulation.reticlehandlersimulation.constant.LogEntryType;
import com.simulation.reticlehandlersimulation.model.LogObject;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class LogHandlerService {

    private final SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MM/dd/yyyy hh:mm:ss");
    
    public List<LogObject> getLogObjectFromLogData(String[] logData){
        List<LogObject> logObjects = new ArrayList<>();
        for(String logEntryString: logData){
            LogObject logObject = getLogObjectFromEntry(logEntryString.trim());
            logObjects.add(logObject);
        }
        return logObjects;
    }
    
    private LogObject getLogObjectFromEntry(String entryString){
        if(entryString.length()>19){
            LogObject logObject = new LogObject();
            logObject.setObjectTime(getDateTimeFromString(entryString.substring(0, 19)));
            logObject.setObjectType(getObjectTypeFromLogEntryString(entryString));
            if(logObject.getObjectType().equals(LogEntryType.SdrRequest.name())){
                logObject.setData(getReticleDataFromEntryString(entryString));
            }
            else{
                logObject.setData("");
            }
            return logObject;
        }
        else{
            return null;
        }
    }
    
    private Date getDateTimeFromString(String dateString){
        try {
            return simpleDateFormat.parse(dateString);
        } catch (ParseException ex) {
            System.out.println("Error parsing log entry date.");
        }
        return null;
    }
    
    private String getObjectTypeFromLogEntryString(String entryString){
        int initialPosition = entryString.indexOf(") ")+2;
        int finalPosition = entryString.indexOf("(", initialPosition);
        return entryString.substring(initialPosition, finalPosition);
    }
    
    private String getReticleDataFromEntryString(String entryString){
        int beginningIndex = entryString.indexOf("Data =")+6;
        return entryString.substring(beginningIndex).trim();
    }
    
}
