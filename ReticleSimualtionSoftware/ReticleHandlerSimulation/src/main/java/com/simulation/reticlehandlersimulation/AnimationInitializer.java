package com.simulation.reticlehandlersimulation;

import com.simulation.reticlehandlersimulation.model.Reticle;
import com.simulation.reticlehandlersimulation.model.ReticlePointer;
import com.simulation.reticlehandlersimulation.model.Station;
import com.simulation.reticlehandlersimulation.service.ReticleHandlerService;
import com.simulation.reticlehandlersimulation.service.ReticlePointerService;
import com.simulation.reticlehandlersimulation.util.StationsInventory;
import java.awt.EventQueue;
import java.io.File;
import java.util.List;
import javax.swing.BoxLayout;
import javax.swing.JFrame;
import javax.swing.JPanel;

public class AnimationInitializer extends JFrame{

    private final ReticleHandlerService reticleHandlerService;
    private final StationsInventory stationsInventory;
    private final ReticlePointerService reticlePointerService;
    
    private final List<Reticle> reticles;
    private final List<Station> stations;
    private final List<ReticlePointer> reticlePointers;
    
    
    public AnimationInitializer(File logFile) {
        stationsInventory = new StationsInventory();
        stations = stationsInventory.getAllStations();
        reticleHandlerService = new ReticleHandlerService(logFile);
        reticles = reticleHandlerService.getReticles();
        reticlePointerService = new ReticlePointerService(reticles, stations);
        reticlePointers = reticlePointerService.getReticlePointers();
        initUI();
    }
    
    private void initUI() {
        JPanel mainPanel = new JPanel();
        mainPanel.setLayout(new BoxLayout(mainPanel, BoxLayout.Y_AXIS));
        mainPanel.add(new ReticlePointersPanel(reticlePointers));
        mainPanel.add(new AnimationFrame(stations, reticles, reticlePointers));
        add(mainPanel);
        setResizable(false);
        pack();
        setTitle("Reticle Handler");    
        setLocationRelativeTo(null);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);        
    }
    
    public void startAnimation(){
        EventQueue.invokeLater(()->{
            this.setVisible(true);
        });
    }
    
}
