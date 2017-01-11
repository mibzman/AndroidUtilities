com.example.sborick.dummytest;

import android.support.v4.app.Fragment;

import org.androidannotations.annotations.EFragment;


@EFragment(R.layout.fragment_test)
public class TestFragment extends Fragment implements TestContract.View {

    TestContract.Presenter presenter;

    @Override
    public void setPresenter(TestContract.Presenter presenter) {
        this.presenter = presenter;
    }
}