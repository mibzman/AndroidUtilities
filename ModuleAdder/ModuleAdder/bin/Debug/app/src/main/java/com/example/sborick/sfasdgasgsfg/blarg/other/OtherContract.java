package com.example.sborick.sfasdgasgsfg.blarg.other;


public interface OtherContract {
    interface View{
        //methods that do something to the ui
        void setPresenter(Presenter presenter);
    }

    interface Presenter{
        //methods that arn't ui
    }
}