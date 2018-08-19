package com.nicolas.smartcityandroid.DAO;

import com.nicolas.smartcityandroid.Exceptions.AddAnimalException;
import com.nicolas.smartcityandroid.Exceptions.AnimalException;
import com.nicolas.smartcityandroid.Model.Animal;

import org.json.JSONException;

import java.util.ArrayList;

public interface IAnimalDAO {
    ArrayList<Animal> getAllAnimals(String nameUser)throws AnimalException, JSONException;
    ArrayList<Animal> jsonToAnimals(String stringJson) throws JSONException;
    Animal GetAnimal() throws AnimalException;
    Animal jsonToAnimal(String json);
    Integer CreateNewAnimal(Animal newAnimal)throws AddAnimalException,JSONException;
}
