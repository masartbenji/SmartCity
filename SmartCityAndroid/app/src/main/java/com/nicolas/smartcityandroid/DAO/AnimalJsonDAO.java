package com.nicolas.smartcityandroid.DAO;

import android.os.NetworkOnMainThreadException;
import android.os.StrictMode;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.nicolas.smartcityandroid.Exceptions.AddAnimalException;
import com.nicolas.smartcityandroid.Exceptions.AnimalException;
import com.nicolas.smartcityandroid.Model.Animal;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.Scanner;

public class AnimalJsonDAO {
    private Gson gsonObject = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd")
            .serializeNulls()
            .create();

    public ArrayList<Animal> getAllAnimals(String nameUser)throws AnimalException, JSONException{
        String  json = "";
        HttpURLConnection connection = null;
        URL url = null;
        InputStream inputStream = null;
        try{
            url = new URL(Constantes.url + "Animal/Person/" + nameUser);
            connection = (HttpURLConnection)url.openConnection();
            Log.i("pk", connection.toString());

            connection.setRequestProperty("Authorization","Bearer " + Constantes.token.getToken());

            connection.setDoInput(true);
            inputStream =  new BufferedInputStream(connection.getInputStream());
            java.util.Scanner scanner = new Scanner(inputStream).useDelimiter("\\A");
            json = scanner.hasNext()?scanner.next():"";
        }
        catch(Exception e) {
            Log.i("pk",e.toString(),e.getCause());
        }
        return jsonToAnimals(json);
    }
    private ArrayList<Animal> jsonToAnimals(String stringJson) throws JSONException {
        ArrayList<Animal> animals = new ArrayList<>();
        JSONArray jsonArray = new JSONArray(stringJson);

        for (int i = 0; i < jsonArray.length();i++){
            JSONObject jsonAnimal = jsonArray.getJSONObject(i);

            animals.add(gsonObject.fromJson(jsonAnimal.toString(),Animal.class));

        }
        return animals;
    }

    public Animal GetAnimal() throws AnimalException {
        String json;
        try{
            URL url = new URL(Constantes.url + "Animal/MaxId");
            HttpURLConnection connection = (HttpURLConnection)url.openConnection();

            connection.setRequestProperty("Authorization","Bearer " + Constantes.token.getToken());

            connection.setDoInput(true);
            InputStream inputStream =  new BufferedInputStream(connection.getInputStream());
            java.util.Scanner scanner = new Scanner(inputStream).useDelimiter("\\A");
            json = scanner.hasNext()?scanner.next():"";
        }
        catch(IOException e) {
            throw new AnimalException();
        }
        return jsonToAnimal(json);
    }

    private Animal jsonToAnimal(String json) {
        return gsonObject.fromJson(json,Animal.class);
    }

    public Integer CreateNewAnimal(Animal newAnimal)throws AddAnimalException,JSONException {
        int code;
        try{
            URL url = new URL(Constantes.url + "Animal/Android");
            HttpURLConnection connection = (HttpURLConnection)url.openConnection();

            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-type","application/json");
            connection.setRequestProperty("Accept","application/json");
            connection.setRequestProperty("Authorization","Bearer " + Constantes.token.getToken());

            connection.setDoInput(true);
            connection.setDoOutput(true);
            connection.connect();

            OutputStream outputStream = connection.getOutputStream();
            OutputStreamWriter streamWriter = new OutputStreamWriter(outputStream);

            String json = gsonObject.toJson(newAnimal,Animal.class);

            streamWriter.write(json);
            streamWriter.flush();
            streamWriter.close();

            code = connection.getResponseCode();
            outputStream.close();
            connection.disconnect();

        }
        catch (IOException e){
            throw new AddAnimalException();
        }
        return code;
    }
}
