package com.nicolas.smartcityandroid.Controller;

import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.nicolas.smartcityandroid.DAO.UserJsonDao;
import com.nicolas.smartcityandroid.Exceptions.ConnectionException;
import com.nicolas.smartcityandroid.Model.TokenReceived;
import com.nicolas.smartcityandroid.Model.UserConnection;
import com.nicolas.smartcityandroid.R;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONException;

public class ConnectionActivity extends AppCompatActivity {

    private EditText login;
    private EditText password;
    private Button confirmButton;
    private NetworkInfo networkInfo;
    private ConnectivityManager connectivityManager;
    private UserJsonDao userJsonDao = new UserJsonDao();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.connection);
        connectivityManager = (ConnectivityManager) ConnectionActivity.this.getSystemService(Context.CONNECTIVITY_SERVICE);
        login = findViewById(R.id.login);
        password = findViewById(R.id.password);
        confirmButton = findViewById(R.id.confirmationButton);
        confirmButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String log = login.getText().toString();
                String pw = password.getText().toString();
                if(!log.equals("") && !pw.equals("")){
                    //Vérif réseau
                    networkInfo = connectivityManager.getActiveNetworkInfo();
                    boolean isConnected = networkInfo != null && networkInfo.isConnectedOrConnecting();
                    if(isConnected){
                        new VerifUser().execute(new UserConnection(log,pw));
                    }
                    else{
                        Toast.makeText(ConnectionActivity.this,"Vous avez perdu votre connection internet",Toast.LENGTH_LONG).show();
                    }
                }
                else{
                    Toast.makeText(ConnectionActivity.this,"Un champ est manquant",Toast.LENGTH_LONG).show();
                }
            }
        });

    }
    private class VerifUser extends AsyncTask<UserConnection,Void,TokenReceived>{
        @Override
        protected TokenReceived doInBackground(UserConnection... userConnections) {

            TokenReceived tokenReceived = new TokenReceived();
            try{
                tokenReceived = userJsonDao.verifUser(userConnections[0]);
            }
            catch (ConnectionException e){
                tokenReceived.setErrorException("Mot de passe ou login invalide");
            }
            catch(JSONException e){
                tokenReceived.setErrorException("Erreur json rencontrée");
            }
            return tokenReceived;
        }

        @Override
        protected void onPostExecute(TokenReceived tokenReceived) {
            super.onPostExecute(tokenReceived);

            if(tokenReceived.getErrorException().equals("") && tokenReceived.getCode() == 200){
                Constantes.token = tokenReceived;
                Intent intent = new Intent(ConnectionActivity.this,AnnouncementsConnectedActivity.class);
                intent.putExtra("nameUser",login.getText().toString());
                startActivity(intent);
            }
        }
    }
    //todo
}
