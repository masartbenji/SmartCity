package com.nicolas.smartcityandroid.DAO;


import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.nicolas.smartcityandroid.Exceptions.AddAnnouncementException;
import com.nicolas.smartcityandroid.Exceptions.AnnouncementsException;
import com.nicolas.smartcityandroid.Model.Announcement;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.Scanner;

public class AnnouncementJsonDAO implements IAnnouncementDAO {
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

    public ArrayList<Announcement> jsonToAnnouncements(String stringJson) throws JSONException {
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

    public Announcement jsonToAnnouncement(String json) {
            return gsonObject.fromJson(json,Announcement.class);
    }

    public int createNewAnnouncement(Announcement announcement)throws AddAnnouncementException,JSONException {
        int code;
        try{
            URL url = new URL(Constantes.url + "Announcement");
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

            String json = gsonObject.toJson(announcement,Announcement.class);

            streamWriter.write(json);
            streamWriter.flush();
            streamWriter.close();

            code = connection.getResponseCode();
            outputStream.close();
            connection.disconnect();

        }
        catch (IOException e){
            throw new AddAnnouncementException();
        }
        return code;
    }

    public ArrayList<Announcement> getAllAnnouncementOfAUser(String nameUser, String nameStatut) throws AnnouncementsException,JSONException {
        String stringJson;
        try{
            URL url = new URL(Constantes.url + "Announcement/" + nameUser + "/" + nameStatut);
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
}
