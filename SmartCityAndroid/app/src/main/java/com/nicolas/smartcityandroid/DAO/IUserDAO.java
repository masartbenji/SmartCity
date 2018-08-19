package com.nicolas.smartcityandroid.DAO;

import com.nicolas.smartcityandroid.Exceptions.ConnectionException;
import com.nicolas.smartcityandroid.Exceptions.InscriptionException;
import com.nicolas.smartcityandroid.Model.TokenReceived;
import com.nicolas.smartcityandroid.Model.User;
import com.nicolas.smartcityandroid.Model.UserConnection;

import org.json.JSONException;

import java.util.ArrayList;

public interface IUserDAO {
    TokenReceived verifUser(UserConnection user)throws ConnectionException, JSONException;
    int InscriptionUser(User user) throws InscriptionException, JSONException;
}
