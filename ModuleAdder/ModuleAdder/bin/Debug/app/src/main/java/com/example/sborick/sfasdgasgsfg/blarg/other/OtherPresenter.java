package com.example.sborick.sfasdgasgsfg.blarg.other;

import android.content.Context;



public class OtherPresenter implements OtherContract.Presenter {

    private final OtherContract.View view;
    private final Context context;

    public OtherPresenter(OtherContract.View otherView, Context otherContext){
        view = otherView;
        context = otherContext;
        view.setPresenter(this);
    }

}