package com.nicolas.smartcityandroid.DAO;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.nicolas.smartcityandroid.Exceptions.BreedException;
import com.nicolas.smartcityandroid.Model.Breed;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.Scanner;

public class BreedJsonDao {
    private Gson gsonObject = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd")
            .serializeNulls()
            .create();
    public Breed getBreed(int id)throws BreedException, JSONException{
        String json = "";
        try{
            URL url = new URL(Constantes.url + "Breed/Android/" +  id);
            HttpURLConnection connection = (HttpURLConnection)url.openConnection();
            Log.i("pk", connection.toString());

            connection.setRequestProperty("Authorization","Bearer " + Constantes.token.getToken());

            connection.setDoInput(true);
            InputStream inputStream =  new BufferedInputStream(connection.getInputStream());
            java.util.Scanner scanner = new Scanner(inputStream).useDelimiter("\\A");
            json = scanner.hasNext()?scanner.next():"";
        }
        catch(Exception e) {
            Log.i("pk",e.toString(),e.getCause());
        }
        return jsonToBreed(json);
    }

    private Breed jsonToBreed(String json)throws JSONException {
        return gsonObject.fromJson(json,Breed.class);
    }

    public ArrayList<Breed> getAllBreed(String nameSpecies) throws BreedException,JSONException {
        String json;
        try{
            URL url = new URL(Constantes.url + "breed/species/" + nameSpecies);
            URLConnection connection = url.openConnection();

            InputStream streamReader = new BufferedInputStream(connection.getInputStream());
            java.util.Scanner scanner = new java.util.Scanner(streamReader).useDelimiter("\\A");
            json = scanner.hasNext()?scanner.next():"";
        }
        catch (IOException e){
            throw new BreedException();
        }
        return jsonToBreeds(json);
    }

    private ArrayList<Breed> jsonToBreeds(String json)throws JSONException {
        ArrayList<Breed> breeds = new ArrayList<>();
        JSONArray jsonArray = new JSONArray(json);
        JSONObject jsonBreed;
        for (int i = 0; i < jsonArray.length();i++){
            jsonBreed = jsonArray.getJSONObject(i);

            breeds.add(gsonObject.fromJson(jsonBreed.toString(),Breed.class));
        }
        return breeds;
    }
}
