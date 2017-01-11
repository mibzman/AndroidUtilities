package com.example.sborick.sfasdgasgsfg.blarg;
                
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.EActivity;

@EActivity(R.layout.activity_blarg)
public class BlargActivity extends AppCompatActivity {

    private BlargPresenter presenter;

    @AfterViews
    void afterViewsLoaded() {
        BlargFragment_ blargFragment = (BlargFragment_) getSupportFragmentManager()
                .findFragmentById(R.id.contentFrame);

        if (blargFragment == null) {
            blargFragment = new BlargFragment_();
            com.example.sborick.sfasdgasgsfg.blarg.ActivityUtils.addFragmentToActivity(getSupportFragmentManager(), blargFragment, R.id.contentFrame);
        }
        presenter = new BlargPresenter(blargFragment, this);
    }
}