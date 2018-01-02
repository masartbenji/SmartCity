package com.example.monpc.smartcity4.View;


import android.Manifest;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;

public class Localisation extends AppCompatActivity implements LocationListener {
    private static final int PERMS_CALL_ID=1234;
    private LocationManager lm;

    @Override
    protected void onResume() {
        super.onResume();
        checkPermissions();
    }
    private void checkPermissions(){
        lm = (LocationManager) this.getSystemService(LOCATION_SERVICE);
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this , new String[]{
                    Manifest.permission.ACCESS_FINE_LOCATION,
                    Manifest.permission.ACCESS_COARSE_LOCATION
            },PERMS_CALL_ID);
            return;
        }
        if (lm.isProviderEnabled(LocationManager.GPS_PROVIDER)) {

            lm.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 0, this);
        }
        if(lm.isProviderEnabled(LocationManager.PASSIVE_PROVIDER)){
            lm.requestLocationUpdates(LocationManager.PASSIVE_PROVIDER,1000,0,this);
        }
        if(lm.isProviderEnabled(LocationManager.NETWORK_PROVIDER)) {
            lm.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 0, this);
        }

    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        if( requestCode==PERMS_CALL_ID){//revenir du bloc qui demande la permission et ensuirte demande si elle est activer
            checkPermissions(); // si pas de droits il redemande
        }

    }

    @Override
    protected void onPause(){
        super.onPause();
        if (lm != null){
            lm.removeUpdates(this);
        }
    }

    @Override
    public void onLocationChanged(Location location) {
        double latitude = location.getLatitude();
        double longitude = location.getLongitude();


    }

    https://www.youtube.com/watch?v=uOKLUu1Jjco&feature=youtu.be
    https://www.youtube.com/watch?v=uOKLUu1Jjco&feature=youtu.be
    https://www.youtube.com/watch?v=uOKLUu1Jjco&feature=youtu.be
    https://www.youtube.com/watch?v=uOKLUu1Jjco&feature=youtu.be
    https://www.youtube.com/watch?v=uOKLUu1Jjco&feature=youtu.be
    https://www.youtube.com/watch?v=uOKLUu1Jjco&feature=youtu.be

    @Override
    public void onStatusChanged(String s, int i, Bundle bundle) {

    }

    @Override
    public void onProviderEnabled(String s) {

    }

    @Override
    public void onProviderDisabled(String s) {

    }
}