package com.nicolas.smartcityandroid.Model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class ApplicationUser implements Serializable {
    @SerializedName("id")
    @NonNull
    private String id;
    @SerializedName("Username")
    @NonNull
    private String name;

    @NonNull
    public String getId() {
        return id;
    }

    public void setId(@NonNull String id) {
        this.id = id;
    }

    @NonNull
    public String getName() {
        return name;
    }

    public void setName(@NonNull String name) {
        this.name = name;
    }

    public ApplicationUser(@NonNull String id, @NonNull String name) {
        setId(id);
        setName(name);
    }

    public ApplicationUser() {
    }
}
