package com.nicolas.smartcityandroid.DAO;

import com.nicolas.smartcityandroid.Exceptions.ColorException;
import com.nicolas.smartcityandroid.Model.Color;

import org.json.JSONException;

import java.util.ArrayList;

public interface IColorDAO {
    ArrayList<Color> getAllColor()throws ColorException,JSONException;
    ArrayList<Color> JsonToColor(String json)throws JSONException;
}
