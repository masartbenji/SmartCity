package com.example.monpc.smartcity4.View;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import com.example.monpc.smartcity4.R;

public class RechercheActivity extends AppCompatActivity{

    private Button btngocalendar;
    private TextView thedate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.recherche_annonce);

        thedate = (TextView) findViewById(R.id.text_date_Rech);
        btngocalendar = (Button) findViewById(R.id.bt_date_Rech);

        Intent incoming = getIntent();
        String date = incoming.getStringExtra("date");
        thedate.setText(date);

        btngocalendar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(RechercheActivity.this,CalendarActivity.class);
                startActivity(intent);
            }
        });
    }


}
