package com.nicolas.smartcityandroid.DAO;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.nicolas.smartcityandroid.Exceptions.StatutException;
import com.nicolas.smartcityandroid.Model.Animal;
import com.nicolas.smartcityandroid.Model.Statut;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.Scanner;

public class StatutJsonDao implements IStatutDAO {
    public ArrayList<Statut> GetAllStatut() throws StatutException, JSONException{
        String json = "";
        try{
            URL url = new URL(Constantes.url + "Status/Android");
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
        return jsonToStatut(json);
    }

    public ArrayList<Statut> jsonToStatut(String json)throws JSONException {
        ArrayList<Statut> statuts = new ArrayList<>();
        JSONArray jsonArray = new JSONArray(json);

        for (int i = 0; i < jsonArray.length();i++){
            JSONObject jsonStatut = jsonArray.getJSONObject(i);
            Gson object = new GsonBuilder().create();

            statuts.add(object.fromJson(jsonStatut.toString(),Statut.class));

        }
        return statuts;
    }
}
