package com.nicolas.smartcityandroid.Model;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.TextClock;
import android.widget.TextView;

import com.nicolas.smartcityandroid.Controller.AnnouncementNoConnectedActivity;
import com.nicolas.smartcityandroid.R;

import java.util.ArrayList;

public class AnnouncementAdapter extends ArrayAdapter<Announcement> {
    public AnnouncementAdapter(@NonNull Context ctx,@NonNull ArrayList<Announcement> announcements) {
        super(ctx, R.layout.listview_announcement,announcements);
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        LayoutInflater layout = LayoutInflater.from(getContext());
        View list = layout.inflate(R.layout.listview_announcement,parent,false);
        Announcement announcement = getItem(position);
        TextView descriptionAnnouncement = list.findViewById(R.id.description);
        descriptionAnnouncement.setText(announcement.getDescription());
        return list;
    }
}
