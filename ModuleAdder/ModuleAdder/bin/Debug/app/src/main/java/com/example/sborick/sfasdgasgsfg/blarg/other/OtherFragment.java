package com.example.sborick.sfasdgasgsfg.blarg.other;

import android.support.v4.app.Fragment;

import org.androidannotations.annotations.EFragment;


@EFragment(R.layout.fragment_other)
public class OtherFragment extends Fragment implements OtherContract.View {

    OtherContract.Presenter presenter;

    @Override
    public void setPresenter(OtherContract.Presenter presenter) {
        this.presenter = presenter;
    }
}