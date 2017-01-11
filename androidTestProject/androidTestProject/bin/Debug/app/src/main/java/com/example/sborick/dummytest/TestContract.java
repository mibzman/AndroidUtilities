com.example.sborick.dummytest;


public interface TestContract {
    interface View{
        //methods that do something to the ui
        void setPresenter(Presenter presenter);
    }

    interface Presenter{
        //methods that arn't ui
    }
}