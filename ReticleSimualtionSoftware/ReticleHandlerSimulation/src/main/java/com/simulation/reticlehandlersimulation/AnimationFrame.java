package com.simulation.reticlehandlersimulation;

import com.simulation.reticlehandlersimulation.model.Reticle;
import com.simulation.reticlehandlersimulation.model.ReticlePointer;
import com.simulation.reticlehandlersimulation.model.Station;
import com.simulation.reticlehandlersimulation.model.Transition;
import com.simulation.reticlehandlersimulation.util.OutputLogWriter;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Polygon;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JPanel;

public class AnimationFrame extends JPanel implements Runnable{

    private final int PANEL_HEIGHT = 720;
    private final int PANEL_WIDTH = 720;
    private final String RETICLE_MOTION_STATUS_START = "START";
    private final String RETICLE_MOTION_STATUS_END = "END";
    
    private final List<Station> stations;
    private final List<Reticle> reticles;
    private final List<ReticlePointer> reticlePointers;
    private final List<ReticlePointer> activeReticlePointers;
    
    private Thread mainThread;
    
    private OutputLogWriter outputLogWriter;
    
    public AnimationFrame(List<Station> stations, List<Reticle> reticles, List<ReticlePointer> reticlePointers) {
        this.stations = stations;
        this.reticles = reticles;
        this.reticlePointers = reticlePointers;
        activeReticlePointers = new ArrayList<>();
        outputLogWriter = new OutputLogWriter();
        initializePanel();
    }
    
    private void initializePanel(){
        setBackground(Color.WHITE);
        setPreferredSize(new Dimension(PANEL_WIDTH, PANEL_HEIGHT));
    }
    
    @Override
    public void addNotify() {
        super.addNotify();
        mainThread = new Thread(this);
        mainThread.start();
    }

    @Override
    protected void paintComponent(Graphics g) {
        super.paintComponent(g);
        drawStations(g);
        drawReticlePointers(g);
        drawReticlePathCovered(g);
        drawReticlePointersTrail(g);
        drawDirectionArrows(g);
    }
    
    private void drawDirectionArrows(Graphics g){
        for(ReticlePointer reticlePointer: activeReticlePointers){
            for(Transition transition: reticlePointer.getFinishedTransitions()){
                int X = (int)(transition.getStartLocationX()+transition.getFinalLocationX())/2+5;
                int Y = (int)(transition.getStartLocationY()+transition.getFinalLocationY())/2+5;
                double angle = findAngle(transition);
                Graphics2D g2d = (Graphics2D) g.create();
                g2d.setColor(reticlePointer.getReticleColor());
                g2d.translate(X, Y);
                g2d.rotate(angle);
                g2d.fillPolygon(getTriangle());
                g2d.dispose();
            }
        }
    }
    
    private Polygon getTriangle(){
        Polygon triangle = new Polygon();
        triangle.addPoint(0, 0);
        triangle.addPoint(-10, 10);
        triangle.addPoint(10, 10);
        return triangle;
    }
    
    private double findAngle(Transition transition){
        return Math.PI/2+Math.atan2((transition.getFinalLocationY()-transition.getStartLocationY()), (transition.getFinalLocationX()-transition.getStartLocationX()));
    }
   
    private void drawReticlePathCovered(Graphics g){
        int i=5;
        for(ReticlePointer reticlePointer: activeReticlePointers){
            g.setColor(reticlePointer.getReticleColor());
            for(Transition transition: reticlePointer.getFinishedTransitions()){
                g.drawLine(transition.getStartLocationX()+i, transition.getStartLocationY()+i, (int)transition.getFinalLocationX()+i, (int)transition.getFinalLocationY()+i);
            }
            //i++;
        }
    }
    
    private void drawReticlePointersTrail(Graphics g){
        for(ReticlePointer reticlePointer: activeReticlePointers){
            g.setColor(reticlePointer.getReticleColor());
            g.drawLine(reticlePointer.getActiveTransition().getStartLocationX()+5, reticlePointer.getActiveTransition().getStartLocationY()+5, (int)reticlePointer.getCurrentLocationX()+5, (int)reticlePointer.getCurrentLocationY()+5);
        }
    }
    
    private void drawStations(Graphics g){
        for(Station station: stations){
            g.setColor(Color.GRAY);
            g.fillRect(station.getX(), station.getY(), station.getWidth(), station.getHeight());
            g.setColor(Color.BLACK);
            g.drawString(station.getName(), station.getX()+10, station.getY()+station.getHeight()/2);
        }
    }
    
    private void drawReticlePointers(Graphics g){
        for(ReticlePointer reticlePointer: activeReticlePointers){
            g.setColor(reticlePointer.getReticleColor());
            g.fillRect((int)reticlePointer.getCurrentLocationX(), (int)reticlePointer.getCurrentLocationY(), 10, 10);
        }
    }
    
    private boolean reachedFinalPosition(ReticlePointer reticlePointer, Transition transition){
        if((int)transition.getFinalLocationX() == Math.round(transition.getInitialLocationX()) && (int)transition.getFinalLocationY() == Math.round(transition.getInitialLocationY())){
            reticlePointer.getFinishedTransitions().add(transition);
            return false;
        }
        return true;
    }
    
    private void movePoint(ReticlePointer reticlePointer, Transition transition){
        if(transition.getTravelTime()==0){
            transition.setInitialLocationX(transition.getFinalLocationX());
            transition.setInitialLocationY(transition.getFinalLocationY());
        }
        else{
            transition.setInitialLocationX(transition.getInitialLocationX()+transition.getVelocityX()/20.00);
            transition.setInitialLocationY(transition.getInitialLocationY()+transition.getVelocityY()/20.00);
        }
        reticlePointer.setCurrentLocationX(transition.getInitialLocationX());
        reticlePointer.setCurrentLocationY(transition.getInitialLocationY());        
    }
    
    private void initializeTransition(ReticlePointer reticlePointer, Transition transition){
        reticlePointer.setActiveTransition(transition);
        if(transition.getTravelTime()==0){
            reticlePointer.setCurrentLocationX(transition.getFinalLocationX());
            reticlePointer.setCurrentLocationY(transition.getFinalLocationY());
        }
        reticlePointer.setCurrentLocationX(transition.getInitialLocationX());
        reticlePointer.setCurrentLocationY(transition.getInitialLocationY());
    }
    
    @Override
    public void run() {
        for(int i=0; i<reticlePointers.size()-1; i++){
            runReticle(i);
            try {
                //Thread.sleep(1000);
                Thread.sleep(getTimeDiff(reticles.get(i+1).getMotions().get(0).getStartTime(), reticles.get(i).getMotions().get(0).getStartTime()));
            } catch (InterruptedException ex) {
                Logger.getLogger(AnimationFrame.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
        runReticle(reticlePointers.size()-1);
        //outputLogWriter.closeBufferedWriter();
    }

    private void runReticle(int i){
        ReticlePointer reticlePointer = reticlePointers.get(i); 
        activeReticlePointers.add(reticlePointer);
        Thread reticlePointerThread = new Thread(reticlePointers.get(i).getReticleName()){
            public void run(){
                while(true){
                    for(Transition transition: reticlePointer.getTransitions()){
                        initializeTransition(reticlePointer, transition);
                        outputLogWriter.writeToLog(reticlePointer.getReticleName(), transition, RETICLE_MOTION_STATUS_START);
                        while(reachedFinalPosition(reticlePointer, transition)){
                            movePoint(reticlePointer, transition);
                            validate();
                            try {
                                Thread.sleep(50);
                                repaint();
                            } catch (InterruptedException ex) {
                                System.out.println(ex.getMessage());
                            }
                        }
                        outputLogWriter.writeToLog(reticlePointer.getReticleName(), transition, RETICLE_MOTION_STATUS_END);
                    }
                    break;
                }
            }
        };
        reticlePointerThread.start();
    }
    
    private long getTimeDiff(Date date1, Date date2){
        return date1.getTime()-date2.getTime();
    }
    
}