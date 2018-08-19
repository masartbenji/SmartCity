package com.nicolas.smartcityandroid.Controller;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.Toast;

import com.nicolas.smartcityandroid.DAO.AnnouncementJsonDAO;
import com.nicolas.smartcityandroid.Model.Announcement;
import com.nicolas.smartcityandroid.Model.AnnouncementAdapter;
import com.nicolas.smartcityandroid.Model.TokenReceived;
import com.nicolas.smartcityandroid.R;
import com.nicolas.smartcityandroid.Services.Constantes;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;

public class AnnouncementsConnectedActivity extends AppCompatActivity {

    private Button foundButton;
    private Button lostButton;
    private Button spaButton;
    private SearchView searchView;
    private String idSearch;
    private ListView announcementsList;
    private ArrayList<Announcement> listItems= new ArrayList<>();
    private String nameStatus;
    private TokenReceived token;
    private String nameUser;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.general_page);
        token = Constantes.token;
        Bundle bundle = this.getIntent().getExtras();
        nameUser = bundle.getString("nameUser");

        announcementsList = findViewById(R.id.listAnnouncements);
        nameStatus = "Trouvé";
        new LoadAnnouncements().execute();
        foundButton = findViewById(R.id.Trouve);
        foundButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                nameStatus = "Trouvé";
                new LoadAnnouncements().execute();
            }
        });

        lostButton = findViewById(R.id.perdu);
        lostButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                nameStatus = "Perdu";
                new LoadAnnouncements().execute();
            }
        });

        spaButton = findViewById(R.id.a_adopter);
        spaButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                nameStatus = "A adopter";
                new LoadAnnouncements().execute();
            }
        });

        searchView = findViewById(R.id.search);
        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String s) {
                new LoadAnnouncementSearch().execute();
                return false;
            }

            @Override
            public boolean onQueryTextChange(String s) {
                idSearch = s;
                return true;
            }
        });

        announcementsList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position, long l) {
                Intent announcement = new Intent(AnnouncementsConnectedActivity.this,AnnouncementConnectedActivity.class);
                Bundle bundle = new Bundle();
                bundle.putSerializable("Announcement",(Serializable)announcementsList.getItemAtPosition(position));
                announcement.putExtras(bundle);
                startActivity(announcement);
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
                startActivity(new Intent(AnnouncementsConnectedActivity.this,AnnouncementsNoConnectedActivity.class));
                break;
            case R.id.add_announcement:
                Intent intentAddAnnouncement = new Intent(AnnouncementsConnectedActivity.this,AddAnnouncementActivity.class);
                intentAddAnnouncement.putExtra("nameUser",nameUser);
                startActivity(intentAddAnnouncement);
                break;
            case R.id.addAnimal:
                Intent intentAddAnimal = new Intent(AnnouncementsConnectedActivity.this,AddAnimalActivity.class);
                intentAddAnimal.putExtra("nameUser",nameUser);
                startActivity(intentAddAnimal);
                break;

        }
        return true;
    }

    private class LoadAnnouncements extends AsyncTask<Void,Void,ArrayList<Announcement>> {

        @Override
        protected ArrayList<Announcement> doInBackground(Void... strings) {

            AnnouncementJsonDAO announcementDAO = new AnnouncementJsonDAO();
            ArrayList<Announcement> announcements = new ArrayList<>();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    announcements = announcementDAO.getAllAnnouncement(nameStatus);
                }
                else{
                    Toast.makeText(AnnouncementsConnectedActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AnnouncementsConnectedActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){
                //todo
            }
            return announcements;
        }

        @Override
        protected void onPostExecute(ArrayList<Announcement> announcements) {
            super.onPostExecute(announcements);
            AnnouncementAdapter adapter = new AnnouncementAdapter(AnnouncementsConnectedActivity.this,announcements);
            adapter.notifyDataSetChanged();
            announcementsList.setAdapter(adapter);
            listItems = announcements;
        }
    }
    private class LoadAnnouncementSearch extends AsyncTask<Void,Void,ArrayList<Announcement>>{

        @Override
        protected ArrayList<Announcement> doInBackground(Void... strings) {

            AnnouncementJsonDAO announcementDAO = new AnnouncementJsonDAO();
            ArrayList<Announcement> announcements = new ArrayList<>();
            try{
                if(token != null && token.getExpirationDate().after(new Date())) {
                    announcements = announcementDAO.getAnnouncementWithId(idSearch);
                }
                else{
                    Toast.makeText(AnnouncementsConnectedActivity.this, R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(AnnouncementsConnectedActivity.this,AnnouncementsNoConnectedActivity.class));
                }
            }
            catch (Exception e){
                //todo
            }
            return announcements;
        }

        @Override
        protected void onPostExecute(ArrayList<Announcement> announcements) {
            super.onPostExecute(announcements);
            AnnouncementAdapter adapter = new AnnouncementAdapter(AnnouncementsConnectedActivity.this,announcements);
            adapter.notifyDataSetChanged();
            announcementsList.setAdapter(adapter);
            listItems = announcements;
        }
    }
}
