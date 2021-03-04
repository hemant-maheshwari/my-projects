package com.simulation.reticlehandlersimulation.util;

import com.simulation.reticlehandlersimulation.model.Station;
import java.util.ArrayList;
import java.util.List;

public class StationsInventory {

    public List<Station> getAllStations(){
        List<Station> stations = new ArrayList<>();
        
        stations.add(new Station("Station5", 10, 10, 100, 100));
        stations.add(new Station("Station1", 10, 210, 100, 100));
        stations.add(new Station("R1", 10, 410, 100, 100));
        stations.add(new Station("Entry1", 10, 610, 100, 100));
        
        stations.add(new Station("Station4", 210, 10, 100, 100));
        stations.add(new Station("Station3", 210, 210, 100, 100));
        stations.add(new Station("R2", 210, 410, 100, 100));
        stations.add(new Station("Entry2", 210, 610, 100, 100));
        
        stations.add(new Station("Station2", 410, 10, 100, 100));
        stations.add(new Station("L11", 410, 210, 100, 100));
        stations.add(new Station("L12", 410, 410, 100, 100));
        stations.add(new Station("L13", 410, 610, 100, 100));
        
        stations.add(new Station("L21", 610, 210, 100, 100));
        stations.add(new Station("L22", 610, 410, 100, 100));
        stations.add(new Station("L23", 610, 610, 100, 100));
        
        return stations;
    }
    
}
