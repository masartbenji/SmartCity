package com.nicolas.smartcityandroid.DAO;

import com.nicolas.smartcityandroid.Exceptions.StatutException;
import com.nicolas.smartcityandroid.Model.Statut;

import org.json.JSONException;

import java.util.ArrayList;

public interface IStatutDAO {
    ArrayList<Statut> GetAllStatut() throws StatutException, JSONException;
    ArrayList<Statut> jsonToStatut(String json)throws JSONException;
}
