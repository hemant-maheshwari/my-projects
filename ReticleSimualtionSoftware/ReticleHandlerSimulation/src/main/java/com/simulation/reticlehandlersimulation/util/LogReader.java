package com.simulation.reticlehandlersimulation.util;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;

public class LogReader {

    private final File file;
    
    public LogReader(File file) {
        this.file = file;
    }
   
    public String[] readFile(){
        BufferedReader reader;
        try {
            reader = new BufferedReader(new FileReader(file));
            StringBuilder stringBuilder = new StringBuilder();
            while (true) {
                String line = reader.readLine();
                if (line == null) break;
                stringBuilder.append(line.trim()).append("\n");
            }
            String fileString = stringBuilder.toString().replaceAll("\n\n\n\n", "\n\n").replaceAll("\n\n\n", "\n\n");
            String[] blocks = fileString.split("\n\n");
            reader.close();
            return blocks;
        } catch (IOException e) {
            System.out.println("Error occured while reading log file.");
        }
        return null;
    }
    
}
