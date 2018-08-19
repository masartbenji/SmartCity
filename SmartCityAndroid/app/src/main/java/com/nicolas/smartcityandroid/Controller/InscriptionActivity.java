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
import com.nicolas.smartcityandroid.Exceptions.InscriptionException;
import com.nicolas.smartcityandroid.Model.TokenReceived;
import com.nicolas.smartcityandroid.Model.User;
import com.nicolas.smartcityandroid.R;

import org.json.JSONException;

public class InscriptionActivity extends AppCompatActivity {

    private EditText login;
    private EditText password;
    private EditText email;
    private EditText phone;
    private String role = "user";
    private Button inscriptionButton;
    private NetworkInfo networkInfo;
    private ConnectivityManager connectivityManager;
    private UserJsonDao userJsonDao = new UserJsonDao();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.inscription);

        login = findViewById(R.id.loginInscrption);
        password = findViewById(R.id.passwordInscription);
        email = findViewById(R.id.emailInscription);
        phone = findViewById(R.id.phoneInscription);
        connectivityManager = (ConnectivityManager) InscriptionActivity.this.getSystemService(Context.CONNECTIVITY_SERVICE);

        inscriptionButton = findViewById(R.id.inscriptionButton);
        inscriptionButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!login.getText().equals("") && !password.getText().equals("") && !phone.getText().equals("")){
                    if(isDigit(phone.getText().toString())){
                        //Vérif réseau
                        networkInfo = connectivityManager.getActiveNetworkInfo();
                        boolean isConnected = networkInfo != null && networkInfo.isConnectedOrConnecting();
                        if(isConnected){
                            User user = new User(login.getText().toString(),role,password.getText().toString(),Integer.parseInt(phone.getText().toString()),email.getText().toString());
                            new CreateUser().execute(user);
                        }
                        else{
                            Toast.makeText(InscriptionActivity.this,R.string.errorNoConnected,Toast.LENGTH_LONG).show();
                        }
                    }
                    else{
                        Toast.makeText(InscriptionActivity.this, R.string.errorTelephone,Toast.LENGTH_LONG).show();
                    }
                }
                else{
                    Toast.makeText(InscriptionActivity.this, R.string.errorAdresseMail,Toast.LENGTH_LONG).show();
                }
            }
        });
    }
    public boolean isDigit(String phoneNumber){
        try {
            Integer.parseInt(phoneNumber);
        } catch (NumberFormatException e){
            return false;
        }
        return true;
    }
    private class CreateUser extends AsyncTask<User,Void,TokenReceived>{

        @Override
        protected TokenReceived doInBackground(User... users) {
            TokenReceived tokenReceived = new TokenReceived();
            try{
                tokenReceived.setCode(userJsonDao.InscriptionUser(users[0]));
            }
            catch (InscriptionException e){
                Toast.makeText(InscriptionActivity.this, R.string.errorInscription,Toast.LENGTH_LONG).show();
            }
            catch(JSONException e){
                Toast.makeText(InscriptionActivity.this, R.string.errorJson,Toast.LENGTH_LONG).show();
            }
            return tokenReceived;
        }

        @Override
        protected void onPostExecute(TokenReceived tokenReceived) {
            super.onPostExecute(tokenReceived);
            switch (tokenReceived.getCode()){
                case 200:
                    Toast.makeText(InscriptionActivity.this, R.string.inscriptionRight,Toast.LENGTH_LONG).show();
                    startActivity(new Intent(InscriptionActivity.this,AnnouncementsNoConnectedActivity.class));
                    break;
                case 400:
                    Toast.makeText(InscriptionActivity.this, R.string.userNameAlreadyExist,Toast.LENGTH_LONG).show();
                    break;
                case 500:
                    Toast.makeText(InscriptionActivity.this, R.string.errorServor,Toast.LENGTH_LONG).show();
                    startActivity(new Intent(InscriptionActivity.this,AnnouncementsNoConnectedActivity.class));
                    break;
                default:
                    Toast.makeText(InscriptionActivity.this, tokenReceived.getErrorException(), Toast.LENGTH_LONG).show();
                    break;
            }
        }
    }
}
