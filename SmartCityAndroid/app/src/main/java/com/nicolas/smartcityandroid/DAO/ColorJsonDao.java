package com.nicolas.smartcityandroid.DAO;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.nicolas.smartcityandroid.Exceptions.ColorException;
import com.nicolas.smartcityandroid.Model.Color;
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

public class ColorJsonDao implements IColorDAO {

    private Gson gsonObject = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd")
            .serializeNulls()
            .create();
    public ArrayList<Color> getAllColor()throws ColorException,JSONException{
        String json;
        try{
                URL url = new URL(Constantes.url + "color");
                URLConnection connection = url.openConnection();

                InputStream streamReader = new BufferedInputStream(connection.getInputStream());
                java.util.Scanner scanner = new java.util.Scanner(streamReader).useDelimiter("\\A");
                json = scanner.hasNext()?scanner.next():"";
            }
            catch (IOException e){
                throw new ColorException();
            }
            return JsonToColor(json);
    }

    public ArrayList<Color> JsonToColor(String json)throws JSONException {
        ArrayList<Color> colors = new ArrayList<>();
        JSONArray jsonArray = new JSONArray(json);
        JSONObject jsonColor;
        for (int i = 0; i < jsonArray.length();i++){
            jsonColor = jsonArray.getJSONObject(i);

            colors.add(gsonObject.fromJson(jsonColor.toString(),Color.class));
        }
        return colors;
    }
}
