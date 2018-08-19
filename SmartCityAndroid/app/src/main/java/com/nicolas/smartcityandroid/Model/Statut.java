package com.nicolas.smartcityandroid.Model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class Statut implements Serializable {
    @SerializedName("id")
    @NonNull
    private int id;
    @SerializedName("state")
    @NonNull
    private String state;

    @NonNull
    public int getId() {
        return id;
    }

    public void setId(@NonNull int id) {
        this.id = id;
    }

    @NonNull
    public String getState() {
        return state;
    }

    public void setState(@NonNull String state) {
        this.state = state;
    }

    public Statut(@NonNull int id, @NonNull String state) {
        setId(id);
        setState(state);
    }
}
