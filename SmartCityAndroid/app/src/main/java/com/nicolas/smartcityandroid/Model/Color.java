package com.nicolas.smartcityandroid.Model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;

public class Color implements Serializable {
    @SerializedName("name")
    @NonNull
    private String color;
    @SerializedName("animal")
    private ArrayList<Animal> animals;

    @NonNull
    public String getColor() {
        return color;
    }

    public void setColor(@NonNull String color) {
        color = color;
    }

    public ArrayList<Animal> getAnimals() {
        return animals;
    }

    public void setAnimals(ArrayList<Animal> animals) {
        this.animals = animals;
    }

    public Color(@NonNull String color, ArrayList<Animal> animals) {
        setColor(color);
        setAnimals(animals);
    }
}
