public class Environment{
    string pressIntroText;
    public static Environment instance = null;

    public Environment(){
        //MobileEnvironment();
        DesktopEnvironment();

        instance = this;
    }

    private void MobileEnvironment(){
        this.pressIntroText = "* Presiona en cualquier lado para empezar";
    }

    private void DesktopEnvironment(){
        this.pressIntroText = "* Has click en cualquier lado para empezar";
    }

    public string GetPressIntroText(){
        return pressIntroText;
    }
}