package com.nicolas.smartcityandroid.Controller;

import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
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

public class ListingAnnouncementActivity extends AppCompatActivity {

    private Button foundButton;
    private Button lostButton;
    private Button spaButton;
    private TokenReceived token;
    private String nameUser;
    private String nameStatut;
    private ListView announcementsList;
    private ArrayList<Announcement> listItems= new ArrayList<>();
    private ConnectivityManager connectivityManager;
    private NetworkInfo networkInfo;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.listing_announcement);
        announcementsList = findViewById(R.id.listOwnAnnouncement);
        token = Constantes.token;
        Bundle bundle = this.getIntent().getExtras();
        nameUser = bundle.getString(getString(R.string.nameUser));
        nameStatut = getString(R.string.trouve);
        connectivityManager = (ConnectivityManager) ListingAnnouncementActivity.this.getSystemService(Context.CONNECTIVITY_SERVICE);
        networkInfo = connectivityManager.getActiveNetworkInfo();
        boolean isConnected = networkInfo != null && networkInfo.isConnectedOrConnecting();
        if(isConnected){
            new LoadAnnouncements().execute();

            foundButton = findViewById(R.id.foundButtonListingAnnouncement);
            foundButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    nameStatut = getString(R.string.trouve);
                    new LoadAnnouncements().execute();
                }
            });
            lostButton = findViewById(R.id.lostButtonListingAnnouncement);
            lostButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    nameStatut = getString(R.string.perdu);
                    new LoadAnnouncements().execute();
                }
            });
            spaButton = findViewById(R.id.spaListingAnnouncement);
            spaButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    nameStatut = getString(R.string.a_adopter);
                    new LoadAnnouncements().execute();
                }
            });
            announcementsList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> adapterView, View view, int position, long l) {
                    Intent announcement = new Intent(ListingAnnouncementActivity.this,AnnouncementConnectedActivity.class);
                    Bundle bundle = new Bundle();
                    bundle.putSerializable("Announcement",(Serializable)announcementsList.getItemAtPosition(position));
                    announcement.putExtras(bundle);
                    startActivity(announcement);
                }
            });
        }
        else{
            Toast.makeText(ListingAnnouncementActivity.this,R.string.errorNoConnected,Toast.LENGTH_LONG).show();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        this.getMenuInflater().inflate(R.menu.connected,menu);
        return true;
    }
    private class LoadAnnouncements extends AsyncTask<Void,Void,ArrayList<Announcement>> {

        @Override
        protected ArrayList<Announcement> doInBackground(Void... strings) {

            AnnouncementJsonDAO announcementDAO = new AnnouncementJsonDAO();
            ArrayList<Announcement> announcements = new ArrayList<>();
            try{
                if(token != null && token.getExpirationDate().after(new Date())){
                    announcements = announcementDAO.getAllAnnouncementOfAUser(nameUser,nameStatut);
                }
                else{
                    Toast.makeText(ListingAnnouncementActivity.this,R.string.disconnectMessage,Toast.LENGTH_LONG).show();
                    Constantes.token = null;
                    startActivity(new Intent(ListingAnnouncementActivity.this,AnnouncementsNoConnectedActivity.class));
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
            AnnouncementAdapter adapter = new AnnouncementAdapter(ListingAnnouncementActivity.this,announcements);
            adapter.notifyDataSetChanged();
            announcementsList.setAdapter(adapter);
            listItems = announcements;
        }
    }
}
