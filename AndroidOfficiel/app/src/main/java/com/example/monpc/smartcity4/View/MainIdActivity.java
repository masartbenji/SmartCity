package com.example.monpc.smartcity4.View;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import com.example.monpc.smartcity4.R;

public class MainIdActivity  extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_page_id);
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        this.getMenuInflater().inflate(R.menu.menu_id,menu);
        return true;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.recherche:
                Intent intentToRecherche = new Intent(MainIdActivity.this,RechercheActivity.class);
                startActivity(intentToRecherche);
                return true;
            case R.id.historique:
                Intent intentToHistorique = new Intent(MainIdActivity.this,RechercheActivity.class);
                startActivity(intentToHistorique);
                return true;
            case R.id.addAnnonce:
                Intent intentToAddAnnonce = new Intent(MainIdActivity.this,RechercheActivity.class);
                startActivity(intentToAddAnnonce);
                return true;
            case R.id.remarque:
                Intent intentToRemarque = new Intent(MainIdActivity.this,RemarqueActivity.class);
                startActivity(intentToRemarque);
                return true;
            case R.id.disconnect:
                signOut();
                return true;
            default:
                return onOptionsItemSelected(item);
        }
    }
    private void  signOut(){
        // todo
    }

}
