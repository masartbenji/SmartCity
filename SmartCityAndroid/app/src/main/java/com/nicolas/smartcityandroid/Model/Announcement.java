package com.nicolas.smartcityandroid.Model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.Date;

public class Announcement implements Serializable {
    @SerializedName("id")
    @NonNull
    private Integer id;

    @SerializedName("date")
    @NonNull
    private Date date;

    @SerializedName("description")
    @NonNull
    private String description;

    @SerializedName("idAnimal")
    @NonNull
    private Integer idAnimal;

    @SerializedName("idStatut")
    @NonNull
    private Integer idStatut;

    @SerializedName("breed")
    @NonNull
    private String breed;

    @SerializedName("species")
    @NonNull
    private String species;

    @SerializedName("name")
    @NonNull
    private String nameAnimal;

    @SerializedName("color")
    @NonNull
    private String color;

    @NonNull
    public String getColor() {
        return color;
    }

    public void setColor(@NonNull String color) {
        this.color = color;
    }

    @NonNull
    public Integer getId() {
        return id;
    }

    public void setId(@NonNull Integer id) {
        this.id = id;
    }

    @NonNull
    public Date getDate() {
        return date;
    }

    public void setDate(@NonNull Date date) {
        this.date = date;
    }

    @NonNull
    public String getDescription() {
        return description;
    }

    public void setDescription(@NonNull String description) {
        this.description = description;
    }

    @NonNull
    public Integer getIdAnimal() {
        return idAnimal;
    }

    public void setIdAnimal(@NonNull Integer idAnimal) {
        this.idAnimal = idAnimal;
    }

    @NonNull
    public Integer getIdStatut() {
        return idStatut;
    }

    public void setIdStatut(@NonNull Integer idStatut) {
        this.idStatut = idStatut;
    }

    @NonNull
    public String getBreed() {
        return breed;
    }

    public void setBreed(@NonNull String breed) {
        this.breed = breed;
    }

    @NonNull
    public String getSpecies() {
        return species;
    }

    public void setSpecies(@NonNull String species) {
        this.species = species;
    }

    @NonNull
    public String getNameAnimal() {
        return nameAnimal;
    }

    public void setNameAnimal(@NonNull String nameAnimal) {
        this.nameAnimal = nameAnimal;
    }

    public Announcement(@NonNull Integer id, @NonNull Date date, @NonNull String description, @NonNull Integer idAnimal, @NonNull Integer idStatut, @NonNull String breed, @NonNull String species, @NonNull String nameAnimal,@NonNull String color) {
        setId(id);
        setDate(date);
        setDescription(description);
        setIdAnimal(idAnimal);
        setIdStatut(idStatut);
        setBreed(breed);
        setBreed(breed);
        setNameAnimal(nameAnimal);
        setColor(color);
    }
}



