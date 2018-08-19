package com.nicolas.smartcityandroid.DAO;

import com.nicolas.smartcityandroid.Exceptions.SpeciesException;
import com.nicolas.smartcityandroid.Model.Species;

import org.json.JSONException;

import java.util.ArrayList;

public interface ISpeciesDAO {
    ArrayList<Species> getAllSpecies() throws SpeciesException,JSONException;
    ArrayList<Species> JsonToSpecies(String json) throws JSONException;
}
