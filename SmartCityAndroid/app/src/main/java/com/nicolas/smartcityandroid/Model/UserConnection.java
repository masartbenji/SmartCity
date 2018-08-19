package com.nicolas.smartcityandroid.Model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class UserConnection implements Serializable {
    @SerializedName("userName")
    @NonNull
    private String login;

    @SerializedName("Password")
    @NonNull
    private  String password;

    @NonNull
    public String getLogin() {
        return login;
    }
    public void setLogin(@NonNull String login) {
        this.login = login;
    }

    @NonNull
    public String getPassword() {
        return password;
    }
    public void setPassword(@NonNull String password) {
        this.password = password;
    }

    public UserConnection(@NonNull String login, @NonNull String password) {
        setLogin(login);
        setPassword(password);
    }
}
