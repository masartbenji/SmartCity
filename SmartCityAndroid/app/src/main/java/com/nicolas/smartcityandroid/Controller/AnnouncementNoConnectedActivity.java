package com.nicolas.smartcityandroid.Controller;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.widget.TextView;

import com.nicolas.smartcityandroid.Model.Announcement;
import com.nicolas.smartcityandroid.R;

import java.text.SimpleDateFormat;

public class AnnouncementNoConnectedActivity extends AppCompatActivity {

    private Announcement announcement;
    private TextView name,species,breed,date,color,description;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.announcement);
        Bundle bundle = this.getIntent().getExtras();
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
        this.getMenuInflater().inflate(R.menu.no_connection,menu);
        return true;
    }
}
