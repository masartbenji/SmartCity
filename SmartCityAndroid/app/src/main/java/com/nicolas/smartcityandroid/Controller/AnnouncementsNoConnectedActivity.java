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

import com.nicolas.smartcityandroid.DAO.*;
import com.nicolas.smartcityandroid.Model.Announcement;
import com.nicolas.smartcityandroid.Model.AnnouncementAdapter;
import com.nicolas.smartcityandroid.R;

import java.io.Serializable;
import java.util.ArrayList;

public class AnnouncementsNoConnectedActivity extends AppCompatActivity {

    private Button foundButton;
    private Button lostButton;
    private Button spaButton;
    private SearchView searchView;
    private String idSearch;
    private ListView announcementsList;
    private ArrayList<Announcement> listItems= new ArrayList<>();
    private String nameStatus;

    @Override
    protected void onCreate(Bundle savedInstanceState){
        super.onCreate(savedInstanceState);
        setContentView(R.layout.general_page);
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
                Intent announcement = new Intent(AnnouncementsNoConnectedActivity.this,AnnouncementNoConnectedActivity.class);
                Bundle bundle = new Bundle();
                bundle.putSerializable("Announcement",(Serializable)announcementsList.getItemAtPosition(position));
                announcement.putExtras(bundle);
                startActivity(announcement);
            }
        });
        }
        @Override
        public boolean onCreateOptionsMenu(Menu menu) {
            this.getMenuInflater().inflate(R.menu.no_connection,menu);
            return true;
        }

        @Override
        public boolean onOptionsItemSelected(MenuItem item) {
            switch (item.getItemId()){
                case R.id.connectionButton:
                    startActivity(new Intent(AnnouncementsNoConnectedActivity.this,ConnectionActivity.class));
                    return true;
                case R.id.inscriptionButton:
                    startActivity(new Intent(AnnouncementsNoConnectedActivity.this,InscriptionActivity.class));
                    return true;
                case R.id.questionButton://todo
                    return true;
                default:return super.onOptionsItemSelected(item);
            }
    }
    private class LoadAnnouncements extends AsyncTask<Void,Void,ArrayList<Announcement>>{

        @Override
        protected ArrayList<Announcement> doInBackground(Void... strings) {

            AnnouncementJsonDAO announcementDAO = new AnnouncementJsonDAO();
            ArrayList<Announcement> announcements = new ArrayList<>();
            try{
                announcements = announcementDAO.getAllAnnouncement(nameStatus);
            }
            catch (Exception e){
                //todo
            }
            return announcements;
        }

        @Override
        protected void onPostExecute(ArrayList<Announcement> announcements) {
            super.onPostExecute(announcements);
            AnnouncementAdapter adapter = new AnnouncementAdapter(AnnouncementsNoConnectedActivity.this,announcements);
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
                announcements = announcementDAO.getAnnouncementWithId(idSearch);
            }
            catch (Exception e){
                //todo
            }
            return announcements;
        }

        @Override
        protected void onPostExecute(ArrayList<Announcement> announcements) {
            super.onPostExecute(announcements);
            AnnouncementAdapter adapter = new AnnouncementAdapter(AnnouncementsNoConnectedActivity.this,announcements);
            adapter.notifyDataSetChanged();
            announcementsList.setAdapter(adapter);
            listItems = announcements;
        }
    }

    //todo
}
