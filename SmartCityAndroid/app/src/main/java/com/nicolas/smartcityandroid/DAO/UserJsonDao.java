package com.nicolas.smartcityandroid.DAO;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.nicolas.smartcityandroid.Exceptions.ConnectionException;
import com.nicolas.smartcityandroid.Exceptions.InscriptionException;
import com.nicolas.smartcityandroid.Model.TokenReceived;
import com.nicolas.smartcityandroid.Model.User;
import com.nicolas.smartcityandroid.Model.UserConnection;
import com.nicolas.smartcityandroid.Services.Constantes;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

public class UserJsonDao implements IUserDAO {
    private Gson gsonUser = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd")
            .serializeNulls()
            .create();
    public TokenReceived verifUser(UserConnection user)throws ConnectionException, JSONException{
        TokenReceived tokenReceivedCode = new TokenReceived();
        try{
            URL url = new URL(Constantes.url + "Jwt");
            HttpURLConnection connection = (HttpURLConnection)url.openConnection();

            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-type","application/json");

            connection.setDoOutput(true);
            connection.setDoInput(true);
            connection.connect();

            OutputStream outputStream = connection.getOutputStream();
            OutputStreamWriter streamWriter = new OutputStreamWriter(outputStream);

            streamWriter.write(gsonUser.toJson(user));
            streamWriter.flush();
            streamWriter.close();

            InputStream inputStream = new BufferedInputStream(connection.getInputStream());
            java.util.Scanner scanner = new java.util.Scanner(inputStream).useDelimiter("\\A");
            String token = scanner.hasNext()?scanner.next():"";

            outputStream.close();
            connection.disconnect();

            JSONObject tokenReceived = new JSONObject(token);
            tokenReceivedCode.setToken(tokenReceived.getString("access_token"));

            if(!tokenReceivedCode.getToken().equals("")){
                tokenReceivedCode.setCode(connection.getResponseCode());

                DateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss", Locale.FRENCH);
                Date currentDate = new Date();
                Long durationToken = Long.parseLong(tokenReceived.getString("expires_in"));
                Calendar cal = Calendar.getInstance();
                cal.setTimeInMillis(currentDate.getTime() + durationToken);
                Date expirationDate = cal.getTime();
                dateFormat.format(expirationDate);
                tokenReceivedCode.setExpirationDate(expirationDate);
            }
        }
        catch(IOException e){
            throw new ConnectionException();
        }
        return tokenReceivedCode;
    }
    public int InscriptionUser(User user) throws InscriptionException, JSONException{
        int code;
        try{
            URL url = new URL(Constantes.url + "Account");
            HttpURLConnection connection = (HttpURLConnection)url.openConnection();

            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-type","application/json");
            connection.setRequestProperty("Accept","application/json");

            connection.setDoInput(true);
            connection.setDoOutput(true);
            connection.connect();

            OutputStream outputStream = connection.getOutputStream();
            OutputStreamWriter streamWriter = new OutputStreamWriter(outputStream);

            String json = gsonUser.toJson(user);

            streamWriter.write(json);
            streamWriter.flush();
            streamWriter.close();

            code = connection.getResponseCode();
            outputStream.close();
            connection.disconnect();

        }
        catch (IOException e){
            throw new InscriptionException();
        }
        return code;
    }
}
