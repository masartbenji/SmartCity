package com.nicolas.smartcityandroid.DAO;

import com.nicolas.smartcityandroid.Exceptions.AddAnnouncementException;
import com.nicolas.smartcityandroid.Exceptions.AnnouncementsException;
import com.nicolas.smartcityandroid.Model.Announcement;

import org.json.JSONException;

import java.util.ArrayList;

public interface IAnnouncementDAO {
    ArrayList<Announcement> getAllAnnouncement(String nameStatus) throws AnnouncementsException,JSONException;
    ArrayList<Announcement> getAnnouncementWithId(String id) throws AnnouncementsException,JSONException;
    ArrayList<Announcement> jsonToAnnouncements(String stringJson) throws JSONException;
    Announcement GetMaxId()throws AnnouncementsException,JSONException;
    Announcement jsonToAnnouncement(String json);
    int createNewAnnouncement(Announcement announcement)throws AddAnnouncementException,JSONException;
    ArrayList<Announcement> getAllAnnouncementOfAUser(String nameUser, String nameStatut) throws AnnouncementsException,JSONException;
}
