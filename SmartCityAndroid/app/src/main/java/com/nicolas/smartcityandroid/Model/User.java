package com.nicolas.smartcityandroid.Model;
import android.support.annotation.NonNull;

import java.io.Serializable;
import com.google.gson.annotations.SerializedName;
public class User implements Serializable {
    @SerializedName("UserName")
    @NonNull
    private String userName;
    @SerializedName("RoleName")
    @NonNull
    private String roleName;
    @SerializedName("password")
    @NonNull
    private String password;
    @SerializedName("Phone")
    @NonNull
    private Integer phone;
    @SerializedName("Email")
    @NonNull
    private String email;

    @NonNull
    public String getUserName() {
        return userName;
    }

    public void setUserName(@NonNull String userName) {
        this.userName = userName;
    }

    @NonNull
    public String getRoleName() {
        return roleName;
    }

    public void setRoleName(@NonNull String roleName) {
        this.roleName = roleName;
    }

    @NonNull
    public String getPassword() {
        return password;
    }

    public void setPassword(@NonNull String password) {
        this.password = password;
    }

    @NonNull
    public Integer getPhone() {
        return phone;
    }

    public void setPhone(@NonNull Integer phone) {
        this.phone = phone;
    }

    @NonNull
    public String getEmail() {
        return email;
    }

    public void setEmail(@NonNull String email) {
        this.email = email;
    }

    public User(@NonNull String userName, @NonNull String roleName, @NonNull String password, @NonNull Integer phone, @NonNull String email) {
        setUserName(userName);
        setRoleName(roleName);
        setPassword(password);
        setPhone(phone);
        setEmail(email);
    }
}
