package com.example.monpc.smartcity4.View;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TabHost;


import com.example.monpc.smartcity4.R;

public class MainActivity extends AppCompatActivity {


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





}
