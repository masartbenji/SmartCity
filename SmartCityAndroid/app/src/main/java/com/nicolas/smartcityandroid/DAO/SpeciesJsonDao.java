package com.nicolas.smartcityandroid.DAO;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.nicolas.smartcityandroid.Exceptions.SpeciesException;
import com.nicolas.smartcityandroid.Model.Species;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;

public class SpeciesJsonDao {
    private Gson gsonObject = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd")
            .serializeNulls()
            .create();

    public ArrayList<Species> getAllSpecies() throws SpeciesException,JSONException {
        String json;
        try{
            URL url = new URL(Constantes.url + "species");
            URLConnection connection = url.openConnection();

            InputStream streamReader = new BufferedInputStream(connection.getInputStream());
            java.util.Scanner scanner = new java.util.Scanner(streamReader).useDelimiter("\\A");
            json = scanner.hasNext()?scanner.next():"";
        }
        catch (IOException e){
            throw new SpeciesException();
        }
        return JsonToSpecies(json);
    }

    private ArrayList<Species> JsonToSpecies(String json) throws JSONException {
        ArrayList<Species> species = new ArrayList<>();
        JSONArray jsonArray = new JSONArray(json);
        JSONObject jsonSpecies;
        for (int i = 0; i < jsonArray.length();i++){
            jsonSpecies = jsonArray.getJSONObject(i);

            species.add(gsonObject.fromJson(jsonSpecies.toString(),Species.class));
        }
        return species;
    }
}
