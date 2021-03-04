package com.simulation.reticlehandlersimulation.thread;

import com.simulation.reticlehandlersimulation.model.Path;
import java.util.List;

public class ReticleThread implements Runnable{

    private Thread reticleThread;
    private final String reticleName;
    private final List<Path> paths;

    public ReticleThread(String reticleName, List<Path> paths) {
        this.reticleName = reticleName;
        this.paths = paths;
    }
    
    @Override
    public void run() {
        System.out.println("Running Thread for Reticle:"+ reticleName);
        System.out.println();
        try{
            for(Path path: paths){
                System.out.println("Moving reticle "+reticleName+" from station: "+ path.getFromStation());
                Thread.sleep(path.getTravelTime());
                System.out.println("Moved reticle "+reticleName+" to station: "+path.getToStation());
                System.out.println();
                System.out.println("Reticle took "+path.getTravelTime()/1000+" seconds to move from "+path.getFromStation()+" to "+path.getToStation());
                System.out.println();
            }
            System.out.println("Reticle "+reticleName+" exited.");
            System.out.println("*******************************************");
        }catch(InterruptedException e){
            System.out.println("Reticle "+reticleName+" interrupted.");
        }
    }
    
    public void start(){
        reticleThread = new Thread(this, reticleName);
        reticleThread.start();
    }
   
}
