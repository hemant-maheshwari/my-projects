package com.simulation.reticlehandlersimulation.service;

import com.simulation.reticlehandlersimulation.model.Path;
import com.simulation.reticlehandlersimulation.model.Point;
import com.simulation.reticlehandlersimulation.model.Reticle;
import com.simulation.reticlehandlersimulation.model.ReticlePointer;
import com.simulation.reticlehandlersimulation.model.Station;
import com.simulation.reticlehandlersimulation.model.Transition;
import com.simulation.reticlehandlersimulation.util.RandomColorGenerator;
import java.util.ArrayList;
import java.util.List;

public class ReticlePointerService {

    private RandomColorGenerator randomColorGenerator;
    
    private List<Reticle> reticles;
    private List<Station> stations;
    
    public ReticlePointerService(List<Reticle> reticles, List<Station> stations) {
        this.reticles = reticles;
        this.stations = stations;
        randomColorGenerator = new RandomColorGenerator();
    }

    public List<ReticlePointer> getReticlePointers(){
        List<ReticlePointer> pointers = new ArrayList<>();
        for(Reticle reticle: reticles){
            pointers.add(getPointerForReticle(reticle));
        }
        return pointers;
    }
    
    private ReticlePointer getPointerForReticle(Reticle reticle){
        List<Path> paths = reticle.getPaths();
        double startX = getPoint(paths.get(0).getFromStation()).getX();
        double startY = getPoint(paths.get(0).getFromStation()).getY();
        ReticlePointer reticlePointer = new ReticlePointer(reticle.getName(), randomColorGenerator.getRandomColor(), startX, startY);
        for(Path path: paths){
            reticlePointer.getTransitions().add(getTransition(path));
        }
        if(reticlePointer.getTransitions().size()>0){
            reticlePointer.setActiveTransition(reticlePointer.getTransitions().get(0));
        }else{
            reticlePointer.setActiveTransition(new Transition());
        }
        return reticlePointer;
    }
    
    private Point getPoint(String stationName){
        for(Station station: stations){
            if(station.getName().equals(stationName)){
                return new Point(station.getX(), station.getY());
            }
        }
        return null;
    }
    
    private Transition getTransition(Path path){
        Point p1 = getPoint(path.getFromStation());
        Point p2 = getPoint(path.getToStation());
        return new Transition(p1.getX(), p1.getY(), p2.getX(), p2.getY(), path.getTravelTime());
    }
    
}
