class MeowTrack : IMeow {
    private readonly IMeow meowObj;
    private int callCount;
    public int Count{get => callCount;}

    public MeowTrack(IMeow _meowObj) {
        meowObj = _meowObj;
        callCount = 0;
    }

    public void meow(int amount = 1) {
        if(amount < 1)
            return;
        callCount += amount;
        meowObj.meow(amount);
    }
}