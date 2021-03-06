package com.nicolas.smartcityandroid.Controller;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TextView;

import com.nicolas.smartcityandroid.Model.Announcement;
import com.nicolas.smartcityandroid.R;
import com.nicolas.smartcityandroid.Services.Constantes;

import java.text.SimpleDateFormat;

public class AnnouncementConnectedActivity extends AppCompatActivity {

    private Announcement announcement;
    private TextView name,species,breed,date,color,description;
    private String nameUser;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.announcement);
        Bundle bundle = this.getIntent().getExtras();
        nameUser = bundle.getString(getString(R.string.nameUser));
        announcement = (Announcement) bundle.getSerializable("Announcement");
        name = findViewById(R.id.name);
        name.setText(announcement.getNameAnimal());
        species = findViewById(R.id.species);
        species.setText(announcement.getSpecies());
        breed = findViewById(R.id.breed);
        breed.setText(announcement.getBreed());
        date = findViewById(R.id.date);
        SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yy");
        date.setText(sdf.format(announcement.getDate()));
        color = findViewById(R.id.color);
        color.setText(announcement.getColor());
        description = findViewById(R.id.description);
        description.setText(announcement.getDescription());
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
                startActivity(new Intent(AnnouncementConnectedActivity.this,AnnouncementsNoConnectedActivity.class));
                break;
            case R.id.add_announcement:
                Intent intentAddAnnouncement = new Intent(AnnouncementConnectedActivity.this,AddAnnouncementActivity.class);
                intentAddAnnouncement.putExtra(getString(R.string.nameUser),nameUser);
                startActivity(intentAddAnnouncement);
                break;
            case R.id.addAnimal:
                Intent intentAddAnimal = new Intent(AnnouncementConnectedActivity.this,AddAnimalActivity.class);
                intentAddAnimal.putExtra(getString(R.string.nameUser),nameUser);
                startActivity(intentAddAnimal);
                break;
            case R.id.listinOwnAnnouncement:
                Intent intentListingAnnouncement = new Intent(AnnouncementConnectedActivity.this,ListingAnnouncementActivity.class);
                intentListingAnnouncement.putExtra(getString(R.string.nameUser),nameUser);
                startActivity(intentListingAnnouncement);
                break;
        }
        return true;
    }
}
