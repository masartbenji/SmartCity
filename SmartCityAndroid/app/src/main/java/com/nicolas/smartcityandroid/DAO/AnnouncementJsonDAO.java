package com.nicolas.smartcityandroid.DAO;


import android.renderscript.ScriptGroup;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.nicolas.smartcityandroid.Exceptions.AnnouncementsException;
import com.nicolas.smartcityandroid.Model.Announcement;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.Scanner;

public class AnnouncementJsonDAO implements AnnouncementDAO {
    private Gson gsonObject = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd")
            .serializeNulls()
            .create();

        public ArrayList<Announcement> getAllAnnouncement(String nameStatus) throws AnnouncementsException,JSONException{
            String stringJson;
            try{
                URL url = new URL(Constantes.url + "Announcement/android/" + nameStatus);
                URLConnection connection = url.openConnection();

                InputStream streamReader = new BufferedInputStream(connection.getInputStream());
                java.util.Scanner scanner = new java.util.Scanner(streamReader).useDelimiter("\\A");
                stringJson = scanner.hasNext()?scanner.next():"";
            }
            catch (IOException e){
                throw new AnnouncementsException();
            }
            return jsonToAnnouncements(stringJson);
        }
        public ArrayList<Announcement> getAnnouncementWithId(String id) throws AnnouncementsException,JSONException {
            String stringJson;
            try {
                URL url = new URL(Constantes.url + "Announcement/android/id/" + id);
                URLConnection connection = url.openConnection();

                InputStream streamReader = new BufferedInputStream(connection.getInputStream());
                java.util.Scanner scanner = new java.util.Scanner(streamReader).useDelimiter("\\A");
                stringJson = scanner.hasNext() ? scanner.next() : "";
            } catch (IOException e) {
                throw new AnnouncementsException();
            }
            return jsonToAnnouncements(stringJson);
        }

    private ArrayList<Announcement> jsonToAnnouncements(String stringJson) throws JSONException {
            ArrayList<Announcement> announcements = new ArrayList<>();
            JSONArray jsonArray = new JSONArray(stringJson);
            JSONObject jsonAnnouncement;
            for (int i = 0; i < jsonArray.length();i++){
                jsonAnnouncement = jsonArray.getJSONObject(i);

                announcements.add(gsonObject.fromJson(jsonAnnouncement.toString(),Announcement.class));
            }
            return announcements;
    }

    public Announcement GetMaxId()throws AnnouncementsException,JSONException {
            String json;
        try{
            URL url = new URL(Constantes.url + "Announcement/MaxId");
            HttpURLConnection connection = (HttpURLConnection)url.openConnection();

            connection.setRequestProperty("Authorization","Bearer " + Constantes.token.getToken());

            connection.setDoInput(true);
            InputStream inputStream =  new BufferedInputStream(connection.getInputStream());
            java.util.Scanner scanner = new Scanner(inputStream).useDelimiter("\\A");
            json = scanner.hasNext()?scanner.next():"";
        }
        catch(IOException e) {
            throw new AnnouncementsException();
        }
        return jsonToAnnouncement(json);
    }

    private Announcement jsonToAnnouncement(String json) {
            return gsonObject.fromJson(json,Announcement.class);
    }
}
