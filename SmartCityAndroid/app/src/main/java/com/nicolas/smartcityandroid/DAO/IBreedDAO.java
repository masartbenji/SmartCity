package com.nicolas.smartcityandroid.DAO;

import com.nicolas.smartcityandroid.Exceptions.BreedException;
import com.nicolas.smartcityandroid.Model.Breed;

import org.json.JSONException;

import java.util.ArrayList;

public interface IBreedDAO {
    Breed getBreed(int id)throws BreedException, JSONException;
    Breed jsonToBreed(String json)throws JSONException;
    ArrayList<Breed> getAllBreed(String nameSpecies) throws BreedException,JSONException;
    ArrayList<Breed> jsonToBreeds(String json)throws JSONException;
}
