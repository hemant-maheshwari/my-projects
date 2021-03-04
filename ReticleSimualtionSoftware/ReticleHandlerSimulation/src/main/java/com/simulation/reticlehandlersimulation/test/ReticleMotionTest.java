package com.simulation.reticlehandlersimulation.test;

import com.simulation.reticlehandlersimulation.model.Reticle;
import com.simulation.reticlehandlersimulation.thread.ReticleThread;
import java.util.Date;
import java.util.List;

public class ReticleMotionTest {

    private final List<Reticle> reticles;
    
    public ReticleMotionTest(List<Reticle> reticles) {
        this.reticles = reticles;
    }
    
    public void test(){
        ReticleThread reticleThread = new ReticleThread(reticles.get(0).getName(), reticles.get(0).getPaths());
        reticleThread.start();
    }
    
    public void testMulti(){
        for(int i=0; i<reticles.size()-1; i++){
            new ReticleThread(reticles.get(i).getName(), reticles.get(i).getPaths()).start();
            try {
                Thread.sleep(getTimeDiff(reticles.get(i+1).getMotions().get(0).getStartTime(), reticles.get(i).getMotions().get(0).getStartTime()));
            } catch (InterruptedException ex) {
                System.out.println("Error handling multiple reticles.");
            }
        }
        new ReticleThread(reticles.get(reticles.size()-1).getName(), reticles.get(reticles.size()-1).getPaths()).start();
    }
    
    private long getTimeDiff(Date date1, Date date2){
        return date1.getTime()-date2.getTime();
    }
    
}
