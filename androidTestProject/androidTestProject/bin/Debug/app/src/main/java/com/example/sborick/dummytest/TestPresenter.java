com.example.sborick.dummytest;

import android.content.Context;



public class TestPresenter implements TestContract.Presenter {

    private final TestContract.View view;
    private final Context context;

    public TestPresenter(TestContract.View testView, Context testContext){
        view = mainView;
        context = testContext;
        view.setPresenter(this);
    }

}