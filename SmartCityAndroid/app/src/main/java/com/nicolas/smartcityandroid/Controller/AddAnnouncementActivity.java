package com.nicolas.smartcityandroid.Controller;

import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import com.nicolas.smartcityandroid.DAO.AnimalJsonDAO;
import com.nicolas.smartcityandroid.DAO.AnnouncementJsonDAO;
import com.nicolas.smartcityandroid.DAO.BreedJsonDao;
import com.nicolas.smartcityandroid.DAO.StatutJsonDao;
import com.nicolas.smartcityandroid.Exceptions.AddAnnouncementException;
import com.nicolas.smartcityandroid.Model.Animal;
import com.nicolas.smartcityandroid.Model.Announcement;
import com.nicolas.smartcityandroid.Model.Breed;
import com.nicolas.smartcityandroid.Model.Statut;
import com.nicolas.smartcityandroid.Model.TokenReceived;
import com.nicolas.smartcityandroid.R;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONException;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;

public class AddAnnouncementActivity extends AppCompatActivity {

    private String nameUser;
    private Spinner animalSpinner;
    private Spinner statutSpinner;
    private EditText description;
    private Integer idNewAnnouncement;
    private Button bouttonAddAnnouncement;
    private List<String> animals = new ArrayList<>();
    private List<String> statuts = new ArrayList<>();
    private List<Animal> animalsList = new ArrayList<>();
    private List<Statut> statutsList = new ArrayList<>();
    private Breed breedOfAnimal;
    private TokenReceived token;
    private NetworkInfo networkInfo;
    private ConnectivityManager connectivityManager;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.add_announcement);
        connectivityManager = (ConnectivityManager) AddAnnouncementActivity.this.getSystemService(Context.CONNECTIVITY_SERVICE);
        Bundle bundle = this.getIntent().getExtras();
        nameUser = bundle.getString("nameUser");
        token = Constantes.token;
        description = findViewById(R.id.descriptionAddAnnouncement);

        networkInfo = connectivityManager.getActiveNetworkInfo();
        boolean isConnected = networkInfo != null && networkInfo.isConnectedOrConnecting();
        if(isConnected){
            new LoadAnimal().execute();
            new LoadStatut().execute();
        }
        bouttonAddAnnouncement = findViewById(R.id.buttonAddAnnouncement);
        bouttonAddAnnouncement.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                new LoadMaxIdAnnouncement().execute();




            }
        });







    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        this.getMenuInflater().inflate(R.menu.connected,menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()){
            case R.id.disconnectButton:
                Constantes.token = null;
                startActivity(new Intent(AddAnnouncementActivity.this,AnnouncementsNoConnectedActivity.class));
                break;
            case R.id.add_announcement:
                Intent intentAddAnnouncement = new Intent(AddAnnouncementActivity.this,AddAnnouncementActivity.class);
                intentAddAnnouncement.putExtra("nameUser",nameUser);
                startActivity(intentAddAnnouncement);
                break;
            case R.id.addAnimal:
                Intent intentAddAnimal = new Intent(AddAnnouncementActivity.this,AddAnimalActivity.class);
                intentAddAnimal.putExtra("nameUser",nameUser);
                startActivity(intentAddAnimal);
                break;

        }
        return true;
    }

    private class LoadAnimal extends AsyncTask<Void,Void,ArrayList<Animal>>{

        @Override
        protected ArrayList<Animal> doInBackground(Void... strings) {
            ArrayList<Animal> animalList = new ArrayList<>();
            AnimalJsonDAO animalJsonDAO = new AnimalJsonDAO();

            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    animalList = animalJsonDAO.getAllAnimals(nameUser);
                }
                else{
                    Toast.makeText(AddAnnouncementActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AddAnnouncementActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){
                Log.i("pk", e.toString());
            }
            return animalList;
        }

        @Override
        protected void onPostExecute(ArrayList<Animal> animalList) {
            super.onPostExecute(animalList);
            for (Animal animal :
                    animalList) {
                animals.add(animal.getName());
            }
            animalsList = animalList;
            animalSpinner = findViewById(R.id.animalSpinner);
            ArrayAdapter<String> adapter = new ArrayAdapter<>(AddAnnouncementActivity.this,android.R.layout.simple_spinner_dropdown_item,animals);
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            adapter.setNotifyOnChange(true);
            animalSpinner.setAdapter(adapter);

        }
    }
    private class LoadStatut extends AsyncTask<Void,Void,ArrayList<Statut>>{

        @Override
        protected ArrayList<Statut> doInBackground(Void... voids) {
            ArrayList<Statut> statutList = new ArrayList<>();
            StatutJsonDao statutJsonDao = new StatutJsonDao();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    statutList = statutJsonDao.GetAllStatut();
                }
                else{
                    Toast.makeText(AddAnnouncementActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AddAnnouncementActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){

            }
            return statutList;
        }

        @Override
        protected void onPostExecute(ArrayList<Statut> statutList) {
            super.onPostExecute(statutList);
            for (Statut statut :
                    statutList) {
                statuts.add(statut.getState());
            }
            statutsList = statutList;
            statutSpinner = findViewById(R.id.statutSpinner);
            ArrayAdapter<String> adapter = new ArrayAdapter<>(AddAnnouncementActivity.this,android.R.layout.simple_spinner_dropdown_item,statuts);
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            adapter.setNotifyOnChange(true);
            statutSpinner.setAdapter(adapter);
        }
    }
    private class LoadMaxIdAnnouncement extends AsyncTask<Void,Void,Announcement>{

        @Override
        protected Announcement doInBackground(Void... voids) {
            AnnouncementJsonDAO announcementJsonDAO = new AnnouncementJsonDAO();
            Announcement announcement = null;
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                   announcement = announcementJsonDAO.GetMaxId();
                }
            }
            catch(Exception e){
                //todo
            }
            return announcement;
        }

        @Override
        protected void onPostExecute(Announcement announcement) {
            super.onPostExecute(announcement);
            idNewAnnouncement = announcement.getId() + 1;
            new LoadBreed().execute(animalsList.get(animalSpinner.getSelectedItemPosition()).getIdBreed());
        }
    }
    private class LoadBreed extends AsyncTask<Integer,Void,Void>{

        @Override
        protected Void doInBackground(Integer... integers) {
            BreedJsonDao breedJsonDao = new BreedJsonDao();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    breedOfAnimal = breedJsonDao.getBreed(integers[0]);
                }
            }
            catch(Exception e){
                //todo
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);
            Animal animal = animalsList.get(animalSpinner.getSelectedItemPosition());
            Statut statut = statutsList.get(statutSpinner.getSelectedItemPosition());
            if(!description.getText().equals("")){
                Announcement newAnnouncement = new Announcement(idNewAnnouncement,new Date(),description.getText().toString(),animal.getId(),statut.getId(),breedOfAnimal.getName(),breedOfAnimal.getSpecies(),animal.getName(),animal.getIdColor());
                new PostAnnouncement().execute(newAnnouncement);
            }
        }
    }
    private class PostAnnouncement extends AsyncTask<Announcement,Void,TokenReceived>{

        @Override
        protected TokenReceived doInBackground(Announcement... announcements) {
            TokenReceived tokenReceived = new TokenReceived();
            AnnouncementJsonDAO announcementJsonDAO = new AnnouncementJsonDAO();
            Announcement announcement = announcements[0];
            try{
                tokenReceived.setCode(announcementJsonDAO.createNewAnnouncement(announcement));
            }
            catch (AddAnnouncementException e){
                //todo
            }
            catch(JSONException e){
                //todo
            }
            return null;
        }
    }
}
