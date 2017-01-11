using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace androidTestProject
{
    public class Files
    {

        //package name is represented with a |
        //module package name is represented with a ^
        //Java module name is represented with a ~
        //lowercase module name is represented with a %


        #region classes
        public const string ACTIVITYUTILS = @"package |;

import android.support.annotation.NonNull;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;


public class ActivityUtils {
    public static void addFragmentToActivity(@NonNull FragmentManager fragmentManager, @NonNull
            Fragment fragment, int frameId) {
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.add(frameId, fragment);
        transaction.commit();
    }
}";


        public const string MAINACTIVITY = @"package ^;
                
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import |.R;
import |.ActivityUtils;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.EActivity;

@EActivity(R.layout.activity_%)
public class ~Activity extends AppCompatActivity {

    private ~Presenter presenter;

    @AfterViews
    void afterViewsLoaded() {
        ~Fragment_ %Fragment = (~Fragment_) getSupportFragmentManager()
                .findFragmentById(R.id.contentFrame);

        if (%Fragment == null) {
            %Fragment = new ~Fragment_();
            ActivityUtils.addFragmentToActivity(getSupportFragmentManager(), %Fragment, R.id.contentFrame);
        }
        presenter = new ~Presenter(%Fragment, this);
    }
}";

        public const string MAINCONTRACT = @"package ^;
import |.R;
import |.ActivityUtils;

public interface ~Contract {
    interface View{
        //methods that do something to the ui
        void setPresenter(Presenter presenter);
    }

    interface Presenter{
        //methods that arn't ui
    }
}";

        public const string MAINFRAGMENT = @"package ^;

import android.support.v4.app.Fragment;

import org.androidannotations.annotations.EFragment;
import |.R;
import |.ActivityUtils;

@EFragment(R.layout.fragment_%)
public class ~Fragment extends Fragment implements ~Contract.View {

    ~Contract.Presenter presenter;

    @Override
    public void setPresenter(~Contract.Presenter presenter) {
        this.presenter = presenter;
    }
}";

        public const string MAINPRESENTER = @"package ^;

import android.content.Context;
import |.R;
import |.ActivityUtils;


public class ~Presenter implements ~Contract.Presenter {

    private final ~Contract.View view;
    private final Context context;

    public ~Presenter(~Contract.View %View, Context %Context){
        view = %View;
        context = %Context;
        view.setPresenter(this);
    }

}";

        #endregion

        #region layout

        //they're both the same for now
        public const string ACTIVITYLAYOUT = @"<?xml version=""1.0"" encoding=""utf-8""?>
<RelativeLayout xmlns:android=""http://schemas.android.com/apk/res/android""
    xmlns:tools=""http://schemas.android.com/tools""
    android:id=""@+id/activity_%""
    android:layout_width=""match_parent""
    android:layout_height=""match_parent"">

    <FrameLayout
        android:id=""@+id/contentFrame""
        android:layout_width=""match_parent""
        android:layout_height=""match_parent""/>
</RelativeLayout>
";

        public const string FRAGMENTLAYOUT = @"<?xml version=""1.0"" encoding=""utf-8""?>
<RelativeLayout xmlns:android=""http://schemas.android.com/apk/res/android""
    xmlns:tools=""http://schemas.android.com/tools""
    android:id=""@+id/fragment_%""
    android:layout_width=""match_parent""
    android:layout_height=""match_parent"">

    <TextView
        android:layout_width=""wrap_content""
        android:layout_height=""wrap_content"" 
        android:text=""Hello World!""/>
</RelativeLayout>
";

        #endregion

        #region build files
        public const string ANDROIDMANIFEST = @"<?xml version=""1.0"" encoding=""utf-8""?>
  <manifest xmlns:android=""http://schemas.android.com/apk/res/android""
    package=""|"" >

    <application
        android:allowBackup=""true""
        android:icon=""@mipmap/ic_launcher""
        android:label=""@string/app_name""
        android:supportsRtl=""true""
        android:theme=""@style/AppTheme"">
 
         <activity android:name="".%.~Activity_"">
   
               <intent-filter>
                   <action android:name=""android.intent.action.MAIN"" />
                     <category android:name=""android.intent.category.LAUNCHER"" />
                   </intent-filter>
               </activity>
           </application>
       </manifest>
       ";

        public const string GRADLE = @"apply plugin: 'com.android.application'
apply plugin: 'android-apt'
def AAVersion = '3.2'

buildscript {
    repositories {
        mavenCentral()
    }
    dependencies {
        classpath 'com.neenbedankt.gradle.plugins:android-apt:1.4'
    }
}

apt {
    arguments {
        androidManifestFile variant.outputs[0].processResources.manifestFile
        resourcePackageName '|'
    }
    }


    android {
    compileSdkVersion 25
    buildToolsVersion ""25.0.1""
    defaultConfig {
        applicationId ""|""
minSdkVersion 15
        targetSdkVersion 25
        versionCode 1
        versionName ""1.0""
        testInstrumentationRunner ""android.support.test.runner.AndroidJUnitRunner""
    }
        buildTypes {
        release {
            minifyEnabled false
            proguardFiles getDefaultProguardFile('proguard-android.txt'), 'proguard-rules.pro'
        }
}
packagingOptions {
        exclude 'META-INF/ASL2.0'
        exclude 'META-INF/LICENSE'
        exclude 'META-INF/NOTICE'
    }
}

dependencies {
    compile fileTree(dir: 'libs', include: ['*.jar'])
    androidTestCompile('com.android.support.test.espresso:espresso-core:2.2.2', {
    exclude group: 'com.android.support', module: 'support-annotations'
    })
    compile 'com.android.support:appcompat-v7:25.0.1'
    compile 'com.couchbase.lite:couchbase-lite-android:1.3.1'
    compile 'com.android.support:support-v4:25.0.1'
    testCompile 'junit:junit:4.12'

    apt ""org.androidannotations:androidannotations:$AAVersion""
    compile ""org.androidannotations:androidannotations-api:$AAVersion""
}";
        #endregion

    }


}
