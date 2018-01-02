package com.example.monpc.smartcity4.View;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TabHost;


import com.example.monpc.smartcity4.R;

import java.lang.reflect.Array;
import java.util.ArrayList;

import Model.Announcement;
import Controller.*;

public class MainActivity extends AppCompatActivity {

    private ListView announcementList;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        TabHost tabHost = findViewById(android.R.id.tabhost);
        tabHost.setup();

        TabHost.TabSpec spec = tabHost.newTabSpec("found");
        spec.setContent(R.id.Trouvé);
        spec.setIndicator("found");
        tabHost.addTab(spec);

        spec = tabHost.newTabSpec("lost");
        spec.setContent(R.id.Perdu);
        spec.setIndicator("lost");
        tabHost.addTab(spec);

        spec = tabHost.newTabSpec("spa");
        spec.setContent(R.id.Adopter);
        spec.setIndicator("spa");
        tabHost.addTab(spec);
        findViewById(R.id.searchButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String recherche = findViewById(R.id.recherche).
            }
        });
        new LoadAnnouncement().execute();
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        this.getMenuInflater().inflate(R.menu.menu_base,menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item){
        switch (item.getItemId()){

            /*case R.id.email:
                Intent intentToMail = new Intent(MainActivity.this,??.class);
                startActivity(intentToMail);
                return true;*/
            case R.id.inscription:
                Intent intentToInscription = new Intent(MainActivity.this,InscriptionActivity.class);
                startActivity(intentToInscription);
                return true;
            case R.id.connexion:
                Intent intentToConnection = new Intent(MainActivity.this,ConnectionActivity.class);
                startActivity(intentToConnection);
                return true;
            default:
                return onOptionsItemSelected(item);
        }

    }
    private class LoadAnnouncement extends AsyncTask<String,Void,ArrayList<Announcement>> {
        @Override
        protected ArrayList<Announcement> doInBackground(String... params ){
            AnnouncementDAO announcementDAO = new AnnouncementDAO();
            ArrayList<Announcement> announcements = new ArrayList<>();
            String status = getStatusSelected();
            try{ announcements = announcementDAO.getAnnouncementsWhereStatusIs(status);}
            catch(Exception e){/**Toast*/}
            return announcements;
        }
        protected String getStatusSelected(){
            if(findViewById(R.id.aAdopter).isSelected()) return "aAdopter";
            else if(findViewById(R.id.trouvé).isSelected()) return "trouvé";
            else if(findViewById(R.id.Perdu).isSelected()) return "perdu";
            return "Probleme";
        }
        @Override
        protected void onPostExecute(ArrayList<Announcement> announcements){
            String []from = new String[]{"image","nomAnimal","espece","race"};
            int[] to = new int[]{R.id.image_annonce,R.id.title_annonce,R.id.espece,R.id.race};
            ListAdapter adapter = new SimpleAdapter(getApplicationContext(),announcements,R.id.announcementRow,from,to);
            announcementList.setAdapter(adapter);
        }
    }




}
