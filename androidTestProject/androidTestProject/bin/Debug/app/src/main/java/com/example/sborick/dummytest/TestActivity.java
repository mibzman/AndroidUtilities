package com.example.sborick.dummytest;
                
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.EActivity;

@EActivity(R.layout.activity_test)
public class TestActivity extends AppCompatActivity {

    private TestPresenter presenter;

    @AfterViews
    void afterViewsLoaded() {
        TestFragment_ testFragment = (TestFragment_) getSupportFragmentManager()
                .findFragmentById(R.id.contentFrame);

        if (testFragment == null) {
            testFragment = new TestFragment_();
            ActivityUtils.addFragmentToActivity(getSupportFragmentManager(), testFragment, R.id.contentFrame);
        }
        presenter = new TestPresenter(testFragment, this);
    }
}