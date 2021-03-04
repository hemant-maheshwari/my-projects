package com.simulation.reticlehandlersimulation;

import com.simulation.reticlehandlersimulation.model.ReticlePointer;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics;
import java.util.List;
import javax.swing.JPanel;

/**
 *
 * @author hemantm
 */
public class ReticlePointersPanel extends JPanel{
    
    private final int PANEL_HEIGHT = 100;
    private final int PANEL_WIDTH = 720;
    
    private final List<ReticlePointer> reticlePointers;

    public ReticlePointersPanel(List<ReticlePointer> reticlePointers) {
        this.reticlePointers = reticlePointers;
        initializePanel();
    }
    
    private void initializePanel(){
        setBackground(Color.WHITE);
        setPreferredSize(new Dimension(PANEL_WIDTH, PANEL_HEIGHT));
    }

    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);
        drawReticlePointers(g);
    }
    
    private void drawReticlePointers(Graphics g){
        int y=10;
        for(ReticlePointer reticlePointer: reticlePointers){
            g.setColor(reticlePointer.getReticleColor());
            g.fillRect(10, y, 10, 10);
            g.drawString(reticlePointer.getReticleName(), 30, y+10);
            y = y + 20;
        }
    }
    
}
