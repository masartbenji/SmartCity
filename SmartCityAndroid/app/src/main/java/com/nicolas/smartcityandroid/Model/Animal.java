package com.nicolas.smartcityandroid.Model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class Animal implements Serializable{
    @SerializedName("id")
    @NonNull
    private int id;
    @SerializedName("name")
    @NonNull
    private String name;
    @SerializedName("idColor")
    @NonNull
    private String idColor;
    @SerializedName("idBreed")
    @NonNull
    private int idBreed;
    @SerializedName("idPerson")
    @NonNull
    private String idPerson;

    public Animal(@NonNull int id, @NonNull String name, @NonNull String idColor, @NonNull int idBreed, @NonNull String idPerson) {
        setId(id);
        setName(name);
        setIdColor(idColor);
        setIdBreed(idBreed);
        setIdPerson(idPerson);
    }

    public Animal() {

    }


    @NonNull
    public int getId() {
        return id;
    }

    public void setId(@NonNull int id) {
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
    public String getIdColor() {
        return idColor;
    }

    public void setIdColor(@NonNull String idColor) {
        this.idColor = idColor;
    }

    @NonNull
    public int getIdBreed() {
        return idBreed;
    }

    public void setIdBreed(@NonNull int idBreed) {
        this.idBreed = idBreed;
    }

    @NonNull
    public String getIdPerson() {
        return idPerson;
    }

    public void setIdPerson(@NonNull String idPerson) {
        this.idPerson = idPerson;
    }
}
