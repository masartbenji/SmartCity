package com.nicolas.smartcityandroid.DAO;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.nicolas.smartcityandroid.Exceptions.ApplicationUserException;
import com.nicolas.smartcityandroid.Model.ApplicationUser;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Scanner;

public class ApplicationUserJsonDao {
    private Gson gsonObject = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd")
            .serializeNulls()
            .create();
    public ApplicationUser GetUser(String userName) throws ApplicationUserException, JSONException {
        String json = "";
        try{
            URL url = new URL(Constantes.url + "Account/Name/" + userName);
            HttpURLConnection connection = (HttpURLConnection)url.openConnection();

            connection.setRequestProperty("Authorization","Bearer " + Constantes.token.getToken());

            connection.setDoInput(true);
            InputStream inputStream =  new BufferedInputStream(connection.getInputStream());
            java.util.Scanner scanner = new Scanner(inputStream).useDelimiter("\\A");
            json = scanner.hasNext()?scanner.next():"";
        }
        catch(IOException e) {
            throw new ApplicationUserException();
        }
        return jsonToPerson(json);
    }

    private ApplicationUser jsonToPerson(String json)throws JSONException {

        Gson object = new GsonBuilder().create();

        return object.fromJson(json,ApplicationUser.class);
    }
}
