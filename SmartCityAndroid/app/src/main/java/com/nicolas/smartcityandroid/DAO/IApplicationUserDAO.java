package com.nicolas.smartcityandroid.DAO;

import com.nicolas.smartcityandroid.Exceptions.ApplicationUserException;
import com.nicolas.smartcityandroid.Model.ApplicationUser;

import org.json.JSONException;

public interface IApplicationUserDAO {
    ApplicationUser GetUser(String userName) throws ApplicationUserException, JSONException;
    ApplicationUser jsonToPerson(String json)throws JSONException;
}
