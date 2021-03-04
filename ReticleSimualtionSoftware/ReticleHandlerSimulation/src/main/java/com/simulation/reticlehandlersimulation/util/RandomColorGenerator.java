package com.simulation.reticlehandlersimulation.util;

import java.awt.Color;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class RandomColorGenerator {

    private int colorIndex = -1;
    private Random random;
    private List<Color> colorList; 

    public RandomColorGenerator() {
        random = new Random();
        colorList = new ArrayList<>();
        colorList.add(Color.BLUE);
        colorList.add(Color.GREEN);
        colorList.add(Color.RED);
        colorList.add(Color.YELLOW);
    }
    
    //public Color getRandomColor(){
//        float r = random.nextFloat();
//        float g = random.nextFloat();
//        float b = random.nextFloat();
//        Color color = new Color(r, g, b);
//        return color.brighter();
//    }
    
    public Color getRandomColor(){
        colorIndex++;
        return colorList.get(colorIndex);
    }
    
}
