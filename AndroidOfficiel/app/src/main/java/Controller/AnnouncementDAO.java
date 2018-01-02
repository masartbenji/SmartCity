package Controller;

import org.json.JSONArray;
import org.json.JSONObject;
import com.google.gson.*;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;

import Model.Announcement;


public class AnnouncementDAO {
    public ArrayList<Announcement> getAllAnnoucements() throws Exception {
        return requeteGetAnnouncement("http://smartcityanimal.azurewebsites.net/api/Announcement");
    }
    public ArrayList<Announcement> getAnnouncementsWhereStatusIs(String status)throws Exception{
        return requeteGetAnnouncement("http://smartcityanimal.azurewebsites.net/api/Announcement/Status");
    }

    public ArrayList<Announcement> requeteGetAnnouncement(String urlString)throws Exception{
        URL url = new URL(urlString);
        HttpURLConnection connection = (HttpURLConnection)url.openConnection();
        if(connection.getResponseCode() == 200){
            /**Toast tout s'est bien passé*/
        }
        connection.setRequestMethod("GET");
        connection.setRequestProperty("Content-type","application/json");
        BufferedReader br = new BufferedReader(new InputStreamReader(connection.getInputStream()));
        StringBuilder sb = new StringBuilder();
        String stringJson = "",line;
        while((line = br.readLine())!= null){
            sb.append(line);}
        br.close();
        stringJson = sb.toString();
        return jsonToAnnouncement(stringJson);
    }

    private ArrayList<Announcement> jsonToAnnouncement(String stringJson)throws Exception {
        ArrayList<Announcement> announcements = new ArrayList<>();
        Announcement announcement;
        JSONArray jsonArray = new JSONArray(stringJson);
        for(int i = 0;i<jsonArray.length();i++){
            JSONObject jsonAnnouncement = jsonArray.getJSONObject(i);
            Gson object = new GsonBuilder().create();
            announcement = object.fromJson(jsonAnnouncement.toString(), Announcement.class);
            announcements.add(announcement);
        }
        return announcements;
    }
}
