package com.nicolas.smartcityandroid.Controller;

import android.content.Context;
import android.content.Intent;
import android.media.session.MediaSession;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import com.nicolas.smartcityandroid.DAO.AnimalJsonDAO;
import com.nicolas.smartcityandroid.DAO.ApplicationUserJsonDao;
import com.nicolas.smartcityandroid.DAO.BreedJsonDao;
import com.nicolas.smartcityandroid.DAO.ColorJsonDao;
import com.nicolas.smartcityandroid.DAO.SpeciesJsonDao;
import com.nicolas.smartcityandroid.Exceptions.AddAnimalException;
import com.nicolas.smartcityandroid.Model.Animal;
import com.nicolas.smartcityandroid.Model.ApplicationUser;
import com.nicolas.smartcityandroid.Model.Breed;
import com.nicolas.smartcityandroid.Model.Color;
import com.nicolas.smartcityandroid.Model.Species;
import com.nicolas.smartcityandroid.Model.TokenReceived;
import com.nicolas.smartcityandroid.R;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONException;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class AddAnimalActivity extends AppCompatActivity {

    private Spinner spinnerColor;
    private List<String> colorList = new ArrayList<>();
    private List<Color> colorsList = new ArrayList<>();
    private Spinner spinnerSpecies;
    private List<String> specList = new ArrayList<>();
    private List<Species> specs = new ArrayList<>();
    private Spinner spinnerBreed;
    private List<String> breedList = new ArrayList<>();
    private List<Breed> breedArrayList = new ArrayList<>();
    private String nameUser;
    private String idUser;
    private Integer idNewAnimal;
    private EditText nameAnimal;
    private Button addAnimalButton;
    private ConnectivityManager connectivityManager;
    private NetworkInfo networkInfo;
    private TokenReceived token;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.add_animal);
        token = Constantes.token;
        connectivityManager = (ConnectivityManager) AddAnimalActivity.this.getSystemService(Context.CONNECTIVITY_SERVICE);
        Bundle bundle = this.getIntent().getExtras();
        nameUser = bundle.getString(getString(R.string.nameUser));

        nameAnimal = findViewById(R.id.nameAddAnimal);
        addAnimalButton = findViewById(R.id.addAnimalButton);
        networkInfo = connectivityManager.getActiveNetworkInfo();
        boolean isConnected = networkInfo != null && networkInfo.isConnectedOrConnecting();
        if(isConnected){
            new LoadColor().execute();
            new LoadSpecies().execute();
            new LoadPerson().execute(nameUser);

            addAnimalButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    String nameAnimalCheck = nameAnimal.getText().toString();
                    String speciesSelected = spinnerSpecies.getSelectedItem().toString();
                    String breedSelected = spinnerBreed.getSelectedItem().toString();
                    String colorSelected = spinnerColor.getSelectedItem().toString();
                    if(!nameAnimalCheck.equals("") && !speciesSelected.equals("") && !breedSelected.equals("") && !colorSelected.equals("")){
                        new LoadAnimal().execute();
                    }
                    else{
                        Toast.makeText(AddAnimalActivity.this, R.string.errorInputField,Toast.LENGTH_LONG).show();
                    }
                }
            });
        }
        else{
            Toast.makeText(AddAnimalActivity.this, R.string.errorNoConnected,Toast.LENGTH_LONG).show();
        }

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
                startActivity(new Intent(AddAnimalActivity.this,AnnouncementsNoConnectedActivity.class));
                break;
            case R.id.add_announcement:
                Intent intentAddAnnouncement = new Intent(AddAnimalActivity.this,AddAnnouncementActivity.class);
                intentAddAnnouncement.putExtra("nameUser",nameUser);
                startActivity(intentAddAnnouncement);
                break;
            case R.id.addAnimal:
                Intent intentAddAnimal = new Intent(AddAnimalActivity.this,AddAnimalActivity.class);
                intentAddAnimal.putExtra("nameUser",nameUser);
                startActivity(intentAddAnimal);
                break;
            case R.id.listinOwnAnnouncement:
                Intent intentListingAnnouncement = new Intent(AddAnimalActivity.this,ListingAnnouncementActivity.class);
                intentListingAnnouncement.putExtra("nameUser",nameUser);
                startActivity(intentListingAnnouncement);
                break;

        }
        return true;
    }

    private class LoadColor extends AsyncTask<Void,Void,ArrayList<Color>>{

        @Override
        protected ArrayList<Color> doInBackground(Void... voids) {
            ArrayList<Color> colors = new ArrayList<>();
            ColorJsonDao colorJsonDao = new ColorJsonDao();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    colors = colorJsonDao.getAllColor();
                }
                else{
                    Toast.makeText(AddAnimalActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AddAnimalActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){
                Toast.makeText(AddAnimalActivity.this, R.string.errorException,Toast.LENGTH_LONG).show();
            }
            return colors;
        }
        @Override
        protected void onPostExecute(ArrayList<Color> colors) {
            super.onPostExecute(colors);
            for (Color color :
                    colors) {
                colorList.add(color.getColor());
            }
            colorsList = colors;
            spinnerColor = findViewById(R.id.colorSpinnerAddAnimal);
            ArrayAdapter<String> adapter = new ArrayAdapter<>(AddAnimalActivity.this,android.R.layout.simple_spinner_dropdown_item,colorList);
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            adapter.setNotifyOnChange(true);
            spinnerColor.setAdapter(adapter);
        }
    }
    private class LoadSpecies extends AsyncTask<Void,Void,ArrayList<Species>>{

        @Override
        protected ArrayList<Species> doInBackground(Void... voids) {
            ArrayList<Species> speciesList = new ArrayList<>();
            SpeciesJsonDao speciesJsonDao = new SpeciesJsonDao();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    speciesList = speciesJsonDao.getAllSpecies();
                }
                else{
                    Toast.makeText(AddAnimalActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AddAnimalActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){
                Toast.makeText(AddAnimalActivity.this, R.string.errorException,Toast.LENGTH_LONG).show();
            }
            return speciesList;
        }

        @Override
        protected void onPostExecute(ArrayList<Species> speciesList) {
            super.onPostExecute(speciesList);
            for (Species species:
                 speciesList) {
                specList.add(species.getName());
            }
            specs = speciesList;
            spinnerSpecies = findViewById(R.id.speciesSpinnerAddAnimal);
            ArrayAdapter<String> adapter = new ArrayAdapter<>(AddAnimalActivity.this,android.R.layout.simple_spinner_dropdown_item,specList);
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            adapter.setNotifyOnChange(true);
            spinnerSpecies.setAdapter(adapter);
            spinnerSpecies.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                @Override
                public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                    new LoadBreed().execute(spinnerSpecies.getSelectedItem().toString());
                }

                @Override
                public void onNothingSelected(AdapterView<?> adapterView) {}
            });

        }
    }
    private class LoadBreed extends AsyncTask<String,Void,ArrayList<Breed>> {

        @Override
        protected ArrayList<Breed> doInBackground(String... strings) {
            ArrayList<Breed> breedList = new ArrayList<>();
            BreedJsonDao breedJsonDao = new BreedJsonDao();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    breedList = breedJsonDao.getAllBreed(strings[0]);
                }
                else{
                    Toast.makeText(AddAnimalActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AddAnimalActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){
                Toast.makeText(AddAnimalActivity.this, R.string.errorException,Toast.LENGTH_LONG).show();
            }
            return breedList;
        }

        @Override
        protected void onPostExecute(ArrayList<Breed> breeds) {
            super.onPostExecute(breeds);
            breedList.clear();
            for (Breed breed:
                    breeds) {
                breedList.add(breed.getName());
            }
            breedArrayList = breeds;
            spinnerBreed = findViewById(R.id.breedSpinnerAddAnimal);
            ArrayAdapter<String> adapter = new ArrayAdapter<>(AddAnimalActivity.this,android.R.layout.simple_spinner_dropdown_item,breedList);
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            adapter.setNotifyOnChange(true);
            spinnerBreed.setAdapter(adapter);
        }
    }
    private class LoadPerson extends AsyncTask<String, Void,ApplicationUser>{

        @Override
        protected ApplicationUser doInBackground(String... strings) {
            String userName = strings[0];
            ApplicationUser user = new ApplicationUser();
            ApplicationUserJsonDao applicationUserJsonDao = new ApplicationUserJsonDao();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    user = applicationUserJsonDao.GetUser(userName);
                }
                else{
                    Toast.makeText(AddAnimalActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AddAnimalActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){
                Toast.makeText(AddAnimalActivity.this, R.string.errorException,Toast.LENGTH_LONG).show();
            }
            return user;
        }

        @Override
        protected void onPostExecute(ApplicationUser applicationUser) {
            super.onPostExecute(applicationUser);
            idUser = applicationUser.getId();
        }
    }
    private class LoadAnimal extends AsyncTask<Void,Void,Animal>{

        @Override
        protected Animal doInBackground(Void... voids) {
            Animal animal = new Animal();
            AnimalJsonDAO animalJsonDAO = new AnimalJsonDAO();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    animal = animalJsonDAO.GetAnimal();
                }
                else{
                    Toast.makeText(AddAnimalActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AddAnimalActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){
                Toast.makeText(AddAnimalActivity.this, R.string.errorException,Toast.LENGTH_LONG).show();
            }
            return animal;
        }

        @Override
        protected void onPostExecute(Animal animal) {
            super.onPostExecute(animal);
            idNewAnimal = animal.getId() + 1;
            new PostAnimal().execute(idNewAnimal);
        }
    }
    private class PostAnimal extends AsyncTask<Integer,Void,TokenReceived>{

        @Override
        protected TokenReceived doInBackground(Integer... integers) {
            TokenReceived tokenReceived = new TokenReceived();
            int idNewAnimal = integers[0];
            AnimalJsonDAO animalJsonDAO = new AnimalJsonDAO();
            Animal newAnimal = new Animal();
            newAnimal.setId(idNewAnimal);
            newAnimal.setIdPerson(idUser);
            newAnimal.setIdBreed(breedArrayList.get(spinnerBreed.getSelectedItemPosition()).getId());
            newAnimal.setIdColor(colorsList.get(spinnerColor.getSelectedItemPosition()).getColor());
            newAnimal.setName(nameAnimal.getText().toString());
            try{
                tokenReceived.setCode(animalJsonDAO.CreateNewAnimal(newAnimal));
            }
            catch (AddAnimalException e){
                Toast.makeText(AddAnimalActivity.this, R.string.errorAddAnimal,Toast.LENGTH_LONG).show();
            }
            catch(JSONException e){
                Toast.makeText(AddAnimalActivity.this, R.string.errorAddAnimal,Toast.LENGTH_LONG).show();
            }
            return null;
        }

        @Override
        protected void onPostExecute(TokenReceived tokenReceived) {
            super.onPostExecute(tokenReceived);
            switch (tokenReceived.getCode()){
                case 200:
                    Toast.makeText(AddAnimalActivity.this,"Votre animal a bien été créé",Toast.LENGTH_LONG).show();
                    startActivity(new Intent(AddAnimalActivity.this,AnnouncementsConnectedActivity.class));
                    break;
                case 400:
                    Toast.makeText(AddAnimalActivity.this,"Il se peut que vous ayez mal rempli certain champs",Toast.LENGTH_LONG).show();
                    break;
                case 500:
                    Toast.makeText(AddAnimalActivity.this,"Une erreur coté serveur est survenue",Toast.LENGTH_LONG).show();
                    startActivity(new Intent(AddAnimalActivity.this,AnnouncementsConnectedActivity.class));
                    break;
                default:
                    Toast.makeText(AddAnimalActivity.this, tokenReceived.getErrorException(), Toast.LENGTH_LONG).show();
                    break;
            }
        }
    }
}
