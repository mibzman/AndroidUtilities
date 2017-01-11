package com.example.sborick.sfasdgasgsfg.blarg.other;
                
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.EActivity;

@EActivity(R.layout.activity_other)
public class OtherActivity extends AppCompatActivity {

    private OtherPresenter presenter;

    @AfterViews
    void afterViewsLoaded() {
        OtherFragment_ otherFragment = (OtherFragment_) getSupportFragmentManager()
                .findFragmentById(R.id.contentFrame);

        if (otherFragment == null) {
            otherFragment = new OtherFragment_();
            ActivityUtils.addFragmentToActivity(getSupportFragmentManager(), otherFragment, R.id.contentFrame);
        }
        presenter = new OtherPresenter(otherFragment, this);
    }
}