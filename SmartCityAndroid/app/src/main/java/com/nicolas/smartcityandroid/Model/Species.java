package com.nicolas.smartcityandroid.Model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;

public class Species implements Serializable {
    @SerializedName("name")
    @NonNull
    private String name;
    @SerializedName("breed")
    private ArrayList<Breed> breed;

    @NonNull
    public String getName() {
        return name;
    }

    public void setName(@NonNull String name) {
        this.name = name;
    }

    public ArrayList<Breed> getBreed() {
        return breed;
    }

    public void setBreed(ArrayList<Breed> breed) {
        this.breed = breed;
    }

    public Species(@NonNull String name, ArrayList<Breed> breed) {
        setName(name);
        setBreed(breed);
    }
}
