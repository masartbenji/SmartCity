package com.nicolas.smartcityandroid.Model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;

public class Breed implements Serializable {
    @SerializedName("id")
    @NonNull
    private Integer id;
    @SerializedName("name")
    @NonNull
    private String name;
    @SerializedName("idSpecies")
    @NonNull
    private String species;
    @SerializedName("idSpeciesNavigation")
    private String idSpeciesNavigation;
    @SerializedName("animal")
    private ArrayList<Animal> animals;

    @NonNull
    public Integer getId() {
        return id;
    }

    public void setId(@NonNull Integer id) {
        this.id = id;
    }

    @NonNull
    public String getName() {
        return name;
    }

    public void setName(@NonNull String name) {
        this.name = name;
    }

    @NonNull
    public String getSpecies() {
        return species;
    }

    public void setSpecies(@NonNull String species) {
        this.species = species;
    }

    public String getIdSpeciesNavigation() {
        return idSpeciesNavigation;
    }

    public void setIdSpeciesNavigation(String idSpeciesNavigation) {
        this.idSpeciesNavigation = idSpeciesNavigation;
    }

    public ArrayList<Animal> getAnimals() {
        return animals;
    }

    public void setAnimals(ArrayList<Animal> animals) {
        this.animals = animals;
    }

    public Breed(@NonNull Integer id, @NonNull String name, @NonNull String species, String idSpeciesNavigation, ArrayList<Animal> animals) {
        setId(id);
        setName(name);
        setSpecies(species);
        setIdSpeciesNavigation(idSpeciesNavigation);
        setAnimals(animals);
    }
}
