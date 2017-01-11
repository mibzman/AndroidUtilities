package com.example.sborick.sfasdgasgsfg.blarg;

import android.content.Context;



public class BlargPresenter implements BlargContract.Presenter {

    private final BlargContract.View view;
    private final Context context;

    public BlargPresenter(BlargContract.View blargView, Context blargContext){
        view = blargView;
        context = blargContext;
        view.setPresenter(this);
    }

}