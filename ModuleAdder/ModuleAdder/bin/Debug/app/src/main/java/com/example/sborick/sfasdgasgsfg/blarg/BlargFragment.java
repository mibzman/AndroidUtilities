package com.example.sborick.sfasdgasgsfg.blarg;

import android.support.v4.app.Fragment;

import org.androidannotations.annotations.EFragment;


@EFragment(R.layout.fragment_blarg)
public class BlargFragment extends Fragment implements BlargContract.View {

    BlargContract.Presenter presenter;

    @Override
    public void setPresenter(BlargContract.Presenter presenter) {
        this.presenter = presenter;
    }
}